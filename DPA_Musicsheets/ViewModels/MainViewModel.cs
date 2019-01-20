using DPA_Musicsheets.Managers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PSAMWPFControlLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DPA_Musicsheets.Commands;
using DPA_Musicsheets.Models;
using Microsoft.Practices.ServiceLocation;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace DPA_Musicsheets.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand FileCommand => new FileCommand();
        public ICommand EditCommand => new EditorCommand();

        private string _fileName;
        private string _currentState;
        private TrackConverter trackConverter = new TrackConverter();

        private TextBox focusedTextBox;

        public MainViewModel()
        {
            trackConverter = new TrackConverter();
            FileName = @"Files/Alle-eendjes-zwemmen-in-het-water.mid";
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

        public TextBox FocusedTextBox
        {
            get { return focusedTextBox; }
            set { focusedTextBox = value; }
        }

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

        public void AddText(string text)
        {
            EditorText += text;
            ServiceLocator.Current.GetInstance<LilypondViewModel>().TextChangedCommand.Execute(null);
            if (focusedTextBox != null)
            {
                focusedTextBox.CaretIndex = focusedTextBox.Text.Length;
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

        public ICommand OpenFileCommand => new RelayCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Midi or LilyPond files (*.mid *.ly)|*.mid;*.ly" };
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                LoadCommand.Execute(null);
            }
        });

        public ICommand LoadCommand => new RelayCommand(() =>
        {
            Track track = trackConverter.GetTrack(FileName);
            track.Print();

            EditorText = trackConverter.ConvertToLilypondText(track);
            ServiceLocator.Current.GetInstance<LilypondViewModel>().ResetHistory();
        });

        #region Focus and key commands, these can be used for implementing hotkeys

        public ICommand OnLostFocusCommand => new RelayCommand(() =>
        {

        });

        public ICommand OnKeyDownCommand => new RelayCommand<KeyEventArgs>((e) =>
        {

        });

        public ICommand OnKeyUpCommand => new RelayCommand<KeyEventArgs>((e) =>
        {

        });

        public ICommand OnWindowClosingCommand => new RelayCommand(() =>
        {
            ViewModelLocator.Cleanup();
        });
        #endregion Focus and key commands, these can be used for implementing hotkeys
    }
}
