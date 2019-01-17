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
            var mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
            var oldFile = mainVM.FileName;
            
            mainVM.OpenFileCommand.Execute(null);
            if (mainVM.FileName != oldFile)
            {
                mainVM.LoadCommand.Execute(null);
            }
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
