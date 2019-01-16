using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DPA_Musicsheets.Commands
{
    public class SimpleCommand : ICommand
    {

        public void Execute(string text)
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Console.WriteLine("Into the barrel and drink what we find!");
            Console.WriteLine(parameter.ToString());

        }

        public event EventHandler CanExecuteChanged;
    }
}
