using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Commands.FileChain
{
    public class SaveToLilyLink : IFileHotkeyChainComponent
    {
        private IFileHotkeyChainComponent next = new SaveToPDFLink();

        public bool ProcessKey(FileAction key)
        {
            if (key == FileAction.SaveToLily)
            {
                ViewModelEvents events = new ViewModelEvents();
                events.SaveToLilypond();
                return true;
            }

            return next.ProcessKey(key);
        }

    }
}
