using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Managers.Components;
using DPA_Musicsheets.Models;
using DPA_Musicsheets.Utils;
using PSAMControlLibrary;
using Note = DPA_Musicsheets.Models.Note;

namespace DPA_Musicsheets.Managers
{
    public class TrackConverter
    {
        private FileHandler fileHandler = new FileHandler();

        public Track GetTrack(string path)
        {
            return fileHandler.readFile(path);
        }

        public List<MusicalSymbol> ConvertToMusicalSymbols(Track track)
        {
            //Note previousNote = new Note(0,0,0,0,0,false);
            List<MusicalSymbol> symbols = new List<MusicalSymbol>();

            symbols.Add(new Clef(ClefType.GClef, 2));
            symbols.Add(new TimeSignature(TimeSignatureType.Numbers, (UInt32)track.DefaultBarContext.BeatsInBar.Item1, 
                (UInt32)track.DefaultBarContext.BeatsInBar.Item2));
            
            //tempo not supported
            //repeat not supported

            foreach (var staff in track.GetStaffs())
            {
                foreach (var bar in staff.GetBars())
                {
                    ConvertBar(ref symbols, bar);
                }
            }

            return symbols;
        }

        private void ConvertBar(ref List<MusicalSymbol> symbols, Bar bar)
        {
            symbols.Add(new Barline());

            foreach (var note in bar.GetNotes())
            {
                if (note.pitch == Pitch.R)
                {
                    symbols.Add(new Rest((MusicalSymbolDuration) note.duration));
                }
                else
                {
                    var psamNote = PsamNote(note, ref symbols);

                    symbols.Add(psamNote);
                }
            }
        }

        private PSAMControlLibrary.Note PsamNote(Note note, ref List<MusicalSymbol> symbols)
        {
            WpfStaffComponent component = new WpfStaffComponent();
            component.AddComponent(new PitchDecorator() {Pitch = note.pitch});
            component.AddComponent(new NoteDurationDecorator() {Length = (int) note.duration});
            component.AddComponent(new AlterDecorator() {MoleOrCross = note.moleOrCross});
            component.AddComponent(new DotsDecorator() {Dots = note.points});
            component.AddComponent(new OctaveDecorator() {Octave = note.octave});

            if (note.hasTilde)
            {
                component.AddComponent(new TildeDecorator());
            }

            NoteValues values = component.ExecuteAll(ref symbols);

            PSAMControlLibrary.Note psamNote = new PSAMControlLibrary.Note(values.NoteStep,
                values.NoteAlter, values.Octave, values.Duration, values.StemDirection,
                values.TieType,
                values.BeamTypes);
            psamNote.NumberOfDots = values.NumberOfDots;
            return psamNote;
        }

        public string convertToLilypondText(Track track)
        {
            return track.ToString();
        }
    }

}

