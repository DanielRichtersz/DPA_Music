using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Commands.FileChain
{
    public class FileOpenLink : IFileHotkeyChainComponent
    {
        private IFileHotkeyChainComponent next = new SaveToLilyLink();

        public bool ProcessKey(FileAction key)
        {
            if (key == FileAction.OpenFile)
            {
                ViewModelEvents events = new ViewModelEvents();
                events.OpenNewFile();
                return true;
            }

            return next.ProcessKey(key);
        }
    }
}
