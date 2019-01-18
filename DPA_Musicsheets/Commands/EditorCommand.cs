using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DPA_Musicsheets.Commands.EditorChain;

namespace DPA_Musicsheets.Commands
{
    public class EditorCommand : ICommand
    {
        IEditorHotkeyLink firstLink = new InsertClefLink();

        public bool CanExecute(object parameter)
        {
            return parameter is EditorAction;
        }

        public void Execute(object parameter)
        {
            var action = (EditorAction)Enum.Parse(typeof(EditorAction), parameter.ToString());
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
