using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Managers.Components
{
    public class DotsDecorator : IWpfDecorator
    {
        public int Dots { get; set; }

        public void Execute(ref NoteValues note, ref List<MusicalSymbol> symbols)
        {
            note.NumberOfDots = Dots;
        }
    }
}
