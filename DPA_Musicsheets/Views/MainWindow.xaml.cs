using Microsoft.Win32;
using PSAMControlLibrary;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DPA_Musicsheets;
using PSAMWPFControlLibrary;
using DPA_Musicsheets.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Key = System.Windows.Input.Key;

namespace DPA_Musicsheets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindowDialog_Closing;
        }

        private void TextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            ServiceLocator.Current.GetInstance<MainViewModel>().FocusedTextBox = tb;
        }
        public void MainWindowDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wilt u de aanpassingen nog opslaan?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
