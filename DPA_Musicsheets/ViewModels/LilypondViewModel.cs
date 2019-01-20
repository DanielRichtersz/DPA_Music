using DPA_Musicsheets.Managers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DPA_Musicsheets.Commands;
using DPA_Musicsheets.Memento;
using DPA_Musicsheets.Utils;

namespace DPA_Musicsheets.ViewModels
{
    public class LilypondViewModel : ViewModelBase
    {
        private MusicLoader _musicLoader;
        private TrackConverter trackConverter;
        private MainViewModel _mainViewModel { get; set; }
        private ViewModelEvents viewModelEvents = new ViewModelEvents();
        private HistoryManager historyManager = new HistoryManager();

        private string _text;
        private string _previousText;
        private string _nextText;

        private int _carretIndex = 0;
        public int Index {
            get { return _carretIndex; }
            set { _carretIndex = value; Console.WriteLine("Index: {0}", value);}
        }
        public HistoryManager HistoryManager { get => historyManager; set => historyManager = value; }
        /// <summary>
        /// This text will be in the textbox.
        /// It can be filled either by typing or loading a file so we only want to set previoustext when it's caused by typing.
        /// </summary>
        public string LilypondText
        {
            get
            {
                return _text;
            }
            set
            {
                if (!_waitingForRender && !_textChangedByLoad)
                {
                    historyManager.AddUndoText(LilypondText);
                }
                _text = value;
                RaisePropertyChanged(() => LilypondText);

            }
        }

        private bool _textChangedByLoad = false;
        private bool _textChangedByUndo = false;
        private DateTime _lastChange;
        private static int MILLISECONDS_BEFORE_CHANGE_HANDLED = 1500;
        private bool _waitingForRender = false;

        public LilypondViewModel(MainViewModel mainViewModel, MusicLoader musicLoader)
        {
            // TODO: Can we use some sort of eventing system so the managers layer doesn't have to know the viewmodel layer and viewmodels don't know each other?
            // And viewmodels don't 
            _mainViewModel = mainViewModel;
            _musicLoader = musicLoader;
            _musicLoader.LilypondViewModel = this;
            
            _text = "Your lilypond text will appear here.";
        }

        public void LilypondTextLoaded(string text)
        {
            _textChangedByLoad = true;
            LilypondText = _previousText = text;
            _textChangedByLoad = false;
            ResetHistory();
        }

        public void ResetHistory()
        {
            historyManager = new HistoryManager();
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }

        public ICommand ButtonInsert => new EditorCommand();

        /// <summary>
        /// This occurs when the text in the textbox has changed. This can either be by loading or typing.
        /// </summary>
        public ICommand TextChangedCommand => new RelayCommand<TextChangedEventArgs>((args) =>
        {
            // If we were typing, we need to do things.
            if (!_textChangedByLoad)
            {
                _waitingForRender = true;
                _lastChange = DateTime.Now;

                _mainViewModel.CurrentState = "Rendering...";

                Task.Delay(MILLISECONDS_BEFORE_CHANGE_HANDLED).ContinueWith((task) =>
                {
                    if ((DateTime.Now - _lastChange).TotalMilliseconds >= MILLISECONDS_BEFORE_CHANGE_HANDLED)
                    {
                        _waitingForRender = false;
                        UndoCommand.RaiseCanExecuteChanged();

                        viewModelEvents.RenderStaffs();

                        if (!_textChangedByUndo)
                        {
                            historyManager.ClearRedo();
                        }
                        else
                        {
                            _textChangedByUndo = false;
                        }

                        _mainViewModel.CurrentState = "";
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext()); // Request from main thread.
            }
        });

        #region Commands for buttons like Undo, Redo and SaveAs
        public RelayCommand UndoCommand => new RelayCommand(() =>
        {
            historyManager.AddRedoText(_text);
            _text = historyManager.GetLastUndoText();
            RaisePropertyChanged(() => LilypondText);
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
            _textChangedByUndo = true;

        }, () => historyManager.UndoAvailable());

        public RelayCommand RedoCommand => new RelayCommand(() =>
        {
            historyManager.AddUndoText(_text);
            _text = historyManager.GetLastRedoText();
            RaisePropertyChanged(() => LilypondText);
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
            _textChangedByUndo = true;

        }, () => historyManager.RedoAvailable());

        public ICommand SaveAsCommand => new RelayCommand(() =>
        {
            // TODO: In the application a lot of classes know which filetypes are supported. Lots and lots of repeated code here...
            // Can this be done better?
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Lilypond|*.ly|PDF|*.pdf" };
            if (saveFileDialog.ShowDialog() == true)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName);
                FileHandler handler = new FileHandler();
                if (handler.SaveFile(saveFileDialog.FileName, _text, extension))
                {
                    ResetHistory();
                }
                else
                {
                    MessageBox.Show($"Extension {extension} is not supported.");
                }
            }
        });

        #endregion Commands for buttons like Undo, Redo and SaveAs
    }
}
