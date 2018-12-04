using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using DPA_Musicsheets.Utils;
using PSAMControlLibrary;
using Note = DPA_Musicsheets.Models.Note;

namespace DPA_Musicsheets.Managers
{
    class TrackConverter
    {
        private static List<Char> notesorder = new List<Char> { 'c', 'd', 'e', 'f', 'g', 'a', 'b' };
        private FileHandler fileHandler = new FileHandler();

        public Track GetTrack(string path)
        {
            return fileHandler.readFile(path);
        }

        public List<MusicalSymbol> convertToMusicalSymbols(Track track)
        {
            Note previousNote = new Note(0,0,0,0,0,false);
            List<MusicalSymbol> symbols = new List<MusicalSymbol>();

            symbols.Add(new Clef(ClefType.GClef, 2));
            symbols.Add(new TimeSignature(TimeSignatureType.Numbers, (UInt32)track.defaultBeatsInBar.Item1, 
                (UInt32)track.defaultBeatsInBar.Item2));
            //tempo not supported


            foreach (var staff in track.GetStaffs())
            {
                


            }

            return symbols;
        }

        public string convertToLilypondText(Track track)
        {
            return "";
        }
    }

}

