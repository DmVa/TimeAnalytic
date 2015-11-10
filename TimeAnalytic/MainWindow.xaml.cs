using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TaskModel.Settings;
using TaskModel.ViewModel;

namespace TimeAnalytic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainViewModel;
        private FileHelper _fileHelper;

        public MainWindow()
        {
            _mainViewModel = new MainWindowViewModel();
            InitializeComponent();
            _fileHelper = new FileHelper();
            this.DataContext = _mainViewModel;
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= MainWindow_Loaded;
            ComboSettings.SelectedItem = _mainViewModel.ActiveConfigutaion;
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = _fileHelper.LoadDataDirectory;
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

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.InitialDirectory = _fileHelper.ExportDataDirectory;
            dlg.Filter = "Excel (*.xlsx)|*.xlsx;|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string errorToShow = _mainViewModel.ExportData(dlg.FileName);
                if (!string.IsNullOrEmpty(errorToShow))
                {
                    MessageBox.Show(errorToShow, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void ButtonManageSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsListWindow settingsListWindow = new SettingsListWindow();
            settingsListWindow.MainViewModel = _mainViewModel;
            settingsListWindow.DataContext = _mainViewModel.Configurations;
            settingsListWindow.InitalSelected = _mainViewModel.ActiveConfigutaion;
            settingsListWindow.Owner = this;
            settingsListWindow.ShowDialog();
            
            if (!_mainViewModel.Configurations.Contains(_mainViewModel.ActiveConfigutaion))
            {
                _mainViewModel.ActiveConfigutaion = null;
            }
        }

        private void ComboSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ugly workaround, combobox at first initialization set default first element.
            if (!this.IsLoaded)
                return;

            ModelSettings selected = ComboSettings.SelectedItem as ModelSettings;
            _mainViewModel.ActiveConfigutaion = selected;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                _mainViewModel.SaveApplicationConfiguration();
                
            }
            catch
            {

            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
