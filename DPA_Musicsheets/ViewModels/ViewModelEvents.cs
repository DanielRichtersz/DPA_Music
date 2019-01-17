using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;

namespace DPA_Musicsheets.ViewModels
{
    public class ViewModelEvents
    {
        public void OpenNewFile()
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().OpenFileCommand.Execute(null);
        }

        public void SaveToLilypond()
        {
            System.Console.WriteLine("Saving to lilypond");
        }

        public void SaveToPDF()
        {
            System.Console.WriteLine("Saving to PDF");
        }

        public void AddTextToEditor(string text)
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().AddText(text);
        }
    }
}
