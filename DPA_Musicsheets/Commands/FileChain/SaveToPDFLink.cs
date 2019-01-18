using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Commands.FileChain
{
    public class SaveToPDFLink : IFileHotkeyChainComponent
    {

        public bool ProcessKey(FileAction key)
        {
            if (key == FileAction.SaveToPdf)
            {
                ViewModelEvents events = new ViewModelEvents();
                events.SaveToPDF();
                return true;
            }

            return false;
        }
    }
}
