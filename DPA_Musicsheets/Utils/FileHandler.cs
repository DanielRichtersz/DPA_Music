using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DPA_Musicsheets.Factories;
using DPA_Musicsheets.Managers.FileLoader;
using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Models;
using Microsoft.Win32;
using Sanford.Multimedia.Midi;
using PSAMControlLibrary;
using DPA_Musicsheets.SaveFiles;

namespace DPA_Musicsheets.Utils
{
    // Loads the file loaders (MidiLoader, LilyPondLoader) to read files
	class FileHandler
	{

		private FileLoaderFactory fileLoaderFactory = new FileLoaderFactory();

		public FileHandler()
		{

			fileLoaderFactory.LoadFactory(".mid", new MidiLoader());
			fileLoaderFactory.LoadFactory(".ly", new LilyPondLoader());

		}

		public Models.Track ReadFile(String path)
		{

			if (!IsMusicFile(path))
			{
				throw new NotSupportedException($"File extension {Path.GetExtension(path)} is not supported.");
			}

			IFileLoader loader = fileLoaderFactory.GetLoader(Path.GetExtension(path));

			return loader.FileToString(path);
		}

		public bool IsMusicFile(String filename)
		{

			string extension = Path.GetExtension(filename);

			return (extension.Equals(".mid") || extension.Equals(".ly"));

		}

	    public string SaveFileDialog(string extension = "*")
	    {
            SaveFileDialog fileDialog = new SaveFileDialog(){Filter = extension};

	        if (fileDialog.ShowDialog() == true)
	        {
	            return fileDialog.FileName;
	        }

	        return null;
	    }

        public bool SaveFile(string fileName, string text, string type)
        {
            Dictionary<string, IFileWriter> fileWriters = new Dictionary<string, IFileWriter>() { { ".ly", new LilypondWriter()}, { ".pdf", new PDFWriter() } };

            var writer = fileWriters[type];

            if(writer != null)
            {
                writer.WriteFile(fileName, text);
                return true;
            }
            return false;
        }
        
        public void SaveToMidi(string fileName, List<MusicalSymbol> WPFStaffs, Models.Track track)
        {
            Sequence sequence = GetSequenceFromWPFStaffs(WPFStaffs, track);

            sequence.Save(fileName);
        }

        private Sequence GetSequenceFromWPFStaffs(List<MusicalSymbol> WPFStaffs, Models.Track track)
        {
            List<string> notesOrderWithCrosses = new List<string>() { "c", "cis", "d", "dis", "e", "f", "fis", "g", "gis", "a", "ais", "b" };
            int absoluteTicks = 0;

            Sequence sequence = new Sequence();

            Sanford.Multimedia.Midi.Track metaTrack = new Sanford.Multimedia.Midi.Track();
            sequence.Add(metaTrack);

            // Calculate tempo
            var bar = (Bar)track.GetStaffs().Last().Bars.Last();
            
            int speed = (60000000 / bar.BarContext.BeatsPerMinute);
            byte[] tempo = new byte[3];
            tempo[0] = (byte)((speed >> 16) & 0xff);
            tempo[1] = (byte)((speed >> 8) & 0xff);
            tempo[2] = (byte)(speed & 0xff);
            metaTrack.Insert(0 /* Insert at 0 ticks*/, new MetaMessage(MetaType.Tempo, tempo));

            Sanford.Multimedia.Midi.Track notesTrack = new Sanford.Multimedia.Midi.Track();
            sequence.Add(notesTrack);

            for (int i = 0; i < WPFStaffs.Count; i++)
            {
                var musicalSymbol = WPFStaffs[i];
                switch (musicalSymbol.Type)
                {
                    case MusicalSymbolType.Note:
                        PSAMControlLibrary.Note note = musicalSymbol as PSAMControlLibrary.Note;

                        // Calculate duration
                        double absoluteLength = 1.0 / (double)note.Duration;
                        absoluteLength += (absoluteLength / 2.0) * note.NumberOfDots;

                        double relationToQuartNote = bar.BarContext.BeatsInBar.Item2/ 4.0;
                        double percentageOfBeatNote = (1.0 / bar.BarContext.BeatsInBar.Item2 ) / absoluteLength;
                        double deltaTicks = (sequence.Division / relationToQuartNote) / percentageOfBeatNote;

                        // Calculate height
                        int noteHeight = notesOrderWithCrosses.IndexOf(note.Step.ToLower()) + ((note.Octave + 1) * 12);
                        noteHeight += note.Alter;
                        notesTrack.Insert(absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 90)); // Data2 = volume

                        absoluteTicks += (int)deltaTicks;
                        notesTrack.Insert(absoluteTicks, new ChannelMessage(ChannelCommand.NoteOn, 1, noteHeight, 0)); // Data2 = volume

                        break;
                    case MusicalSymbolType.TimeSignature:
                        byte[] timeSignature = new byte[4];
                       // timeSignature[0] = (byte)_beatsPerBar;
                        //timeSignature[1] = (byte)(Math.Log(_beatNote) / Math.Log(2));
                        metaTrack.Insert(absoluteTicks, new MetaMessage(MetaType.TimeSignature, timeSignature));
                        break;
                    default:
                        break;
                }
            }

            notesTrack.Insert(absoluteTicks, MetaMessage.EndOfTrackMessage);
            metaTrack.Insert(absoluteTicks, MetaMessage.EndOfTrackMessage);
            return sequence;
        }
    }
}
