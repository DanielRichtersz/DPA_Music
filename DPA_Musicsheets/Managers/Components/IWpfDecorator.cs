using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Models;
using PSAMControlLibrary;

namespace DPA_Musicsheets.Managers.Components
{
    public interface IWpfDecorator
    {
        void Execute(ref NoteValues note, ref List<MusicalSymbol> symbols);
    }
}
