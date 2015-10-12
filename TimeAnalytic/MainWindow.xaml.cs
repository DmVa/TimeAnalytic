using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskModel.DataLoad;
using TaskModel.ViewModel;

namespace TimeAnalytic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainViewModel;
        private string _dataDirectory;
        public MainWindow()
        {
            InitializeComponent();
            _dataDirectory = new FileHelper().DataDirectory;
            _mainViewModel = new MainWindowViewModel();;
            this.DataContext = _mainViewModel;
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = _dataDirectory;
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                _mainViewModel.DataFile = filename;
            }
        }
        private void ButtonLoadData_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.LoadData();
        }

    }
}
