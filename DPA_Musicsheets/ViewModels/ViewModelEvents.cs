﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DPA_Musicsheets.Convertion.LilypondConvertion;
using DPA_Musicsheets.Managers;
using DPA_Musicsheets.Managers.FileLoader;
using DPA_Musicsheets.Models;
using DPA_Musicsheets.Utils;
using Microsoft.Practices.ServiceLocation;

namespace DPA_Musicsheets.ViewModels
{
    public class ViewModelEvents
    {
        MainViewModel mainVM = ServiceLocator.Current.GetInstance<MainViewModel>();
        public void OpenNewFile()
        {
            var oldFile = mainVM.FileName;
            
            mainVM.OpenFileCommand.Execute(null);
            if (mainVM.FileName != oldFile)
            {
                mainVM.LoadCommand.Execute(null);
            }
        }

        public void SaveToPDF()
        {
            var fileHandler = new FileHandler();

            var path = fileHandler.SaveFileDialog("PDF|*.pdf");
            if (path != null)
            {
                fileHandler.SaveToPDF(path, mainVM.EditorText);
            }

            System.Console.WriteLine("Saving to lilypond");
        }

        public void SaveToLilypond()
        { 
            var fileHandler = new FileHandler();

            var path = fileHandler.SaveFileDialog("Lilypond|*.ly");
                if (path != null)
            {
                fileHandler.SaveToLilypond(path, mainVM.EditorText);
            }

            System.Console.WriteLine("Saving to PDF");
        }

        public void AddTextToEditor(string text)
        {
            ServiceLocator.Current.GetInstance<MainViewModel>().AddText(text);
        }

        public void RenderStaffs()
        {
            LilypondConverter converter = new LilypondConverter();

            string[] stringArray = Regex.Replace(mainVM.EditorText, @"\s+", " ").Split(' ');

            Track domainTrack = converter.CreateTrackFromStringParts(stringArray);

            ServiceLocator.Current.GetInstance<StaffsViewModel>().SetStaffs(new TrackConverter().ConvertToMusicalSymbols(domainTrack));
        }
    }
}