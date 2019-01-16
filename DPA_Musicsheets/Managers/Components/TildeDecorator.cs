using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Managers.Components
{
    public class TildeDecorator : IWpfDecorator
    {
        public void Execute(ref NoteValues note, ref List<MusicalSymbol> symbols)
        {
            var lastNote = symbols.Last(s => s is PSAMControlLibrary.Note) as PSAMControlLibrary.Note;
            if (lastNote != null) lastNote.TieType = NoteTieType.Start;
            note.TieType = NoteTieType.Stop;
        }
    }
}
