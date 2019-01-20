using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using PSAMControlLibrary;
using Note = PSAMControlLibrary.Note;

namespace DPA_Musicsheets.Managers.Components
{
    public class OctaveDecorator : IWpfDecorator
    {
        public Octave Octave { get; set; }
        private List<Char> notesorder = new List<Char> { 'C', 'D', 'E', 'F', 'G', 'A', 'B' };
        private static char lastNote = 'c';
        
        public void Execute(ref NoteValues note, ref List<MusicalSymbol> symbols)
        {
            note.Octave = (int) Octave + 4;

            int distanceWithPreviousNote = notesorder.IndexOf(note.NoteStep.ElementAt(0)) - notesorder.IndexOf(lastNote);
            lastNote = note.NoteStep.ElementAt(0);
        }
    }
}
