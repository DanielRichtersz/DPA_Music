using DPA_Musicsheets.Managers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using PSAMWPFControlLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DPA_Musicsheets.Commands;
using DPA_Musicsheets.Models;
using Microsoft.Practices.ServiceLocation;

namespace DPA_Musicsheets.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _fileName;
        private string _currentState;
        private MusicLoader _musicLoader;
        private TrackConverter trackConverter;

        public string EditorText
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LilypondViewModel>().LilypondText;
            }
            set
            {
                RaisePropertyChanged("EditorText");
                ServiceLocator.Current.GetInstance<LilypondViewModel>().LilypondText = value;
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                RaisePropertyChanged(() => FileName);
            }
        }

        /// <summary>
        /// The current state can be used to display some text.
        /// "Rendering..." is a text that will be displayed for example.
        /// </summary>
        public string CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; RaisePropertyChanged(() => CurrentState); }
        }


        public MainViewModel(MusicLoader musicLoader)
        {
            _musicLoader = musicLoader;
            this.trackConverter = new TrackConverter();
            FileName = @"Files/Alle-eendjes-zwemmen-in-het-water.mid";

        }

        public ICommand TestCommand => new SimpleCommand();

        public ICommand OpenFileCommand => new RelayCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Midi or LilyPond files (*.mid *.ly)|*.mid;*.ly" };
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }
        });

        public ICommand LoadCommand => new RelayCommand(() =>
        {
            Track track = trackConverter.GetTrack(FileName);
            track.print();

            EditorText = trackConverter.convertToLilypondText(track);
            ServiceLocator.Current.GetInstance<StaffsViewModel>().SetStaffs(trackConverter.ConvertToMusicalSymbols(track));
                
        });

        #region Focus and key commands, these can be used for implementing hotkeys

        public ICommand OnLostFocusCommand => new RelayCommand(() =>
        {
            Console.WriteLine("Maingrid Lost focus");
        });

        public ICommand OnKeyDownCommand => new RelayCommand<KeyEventArgs>((e) =>
        {
            Console.WriteLine($"Key down: {e.Key}");
        });

        public ICommand OnKeyUpCommand => new RelayCommand<KeyEventArgs>((e) =>
        {
            RaisePropertyChanged("EditorText");
        });

        public ICommand OnWindowClosingCommand => new RelayCommand(() =>
        {
            ViewModelLocator.Cleanup();
        });
        #endregion Focus and key commands, these can be used for implementing hotkeys
    }
}
