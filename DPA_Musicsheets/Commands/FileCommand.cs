using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DPA_Musicsheets.Commands.FileChain;

namespace DPA_Musicsheets.Commands
{
    public class FileCommand : ICommand
    {
        IFileHotkeyChainComponent firstLink = new FileOpenLink();

        public bool CanExecute(object parameter)
        {
            return parameter is FileAction;
        }

        public void Execute(object parameter)
        {
            var action = (FileAction) Enum.Parse(typeof(FileAction), parameter.ToString());
            if (firstLink.ProcessKey(action))
            {
                Console.WriteLine("Did process hotkey");
            }
            else
            {
                Console.WriteLine("Could not process hotkey");
            }

        }

        public event EventHandler CanExecuteChanged;
    }
}
