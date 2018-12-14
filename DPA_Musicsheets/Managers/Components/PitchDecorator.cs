using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Managers.Components
{
    public class PitchDecorator : IWpfDecorator
    {
        public Pitch Pitch { get; set; }

        public void Execute(ref NoteValues note, ref List<MusicalSymbol> symbols)
        {
            note.NoteStep = Enum.GetName(typeof(Pitch), Pitch);
        }
    }
}
