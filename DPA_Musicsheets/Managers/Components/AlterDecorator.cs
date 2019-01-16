using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Managers.Components
{
    public class AlterDecorator : IWpfDecorator
    {
        public MoleOrCross MoleOrCross { get; set; }

        public void Execute(ref NoteValues note, ref List<MusicalSymbol> symbols)
        {
            note.NoteAlter = (int)MoleOrCross - 1;
        }
    }
}
