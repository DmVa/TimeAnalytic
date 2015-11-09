using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskModel.DataLoad;
using TaskModel.Settings;
using TaskModel.ViewModel;

namespace TimeAnalytic
{
    /// <summary>
    /// Interaction logic for SettingsList.xaml
    /// </summary>
    public partial class SettingsListWindow : Window
    {
        private FileHelper _fileHelper;

        public SettingsListWindow()
        {
            _fileHelper = new FileHelper();
            this.Loaded += SettingsListWindow_Loaded;
            InitializeComponent();
        }

        void SettingsListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= SettingsListWindow_Loaded;
            Selected = InitalSelected;
        }

        public MainWindowViewModel MainViewModel { get; set; }

        public Collection<ModelSettings> Items
        {
            get { return (Collection<ModelSettings>)DataContext; }
            set { DataContext = value; }
        }
        

        public ModelSettings Selected
        {
            get { return SettingsList.SelectedItem as ModelSettings; }
            set { SettingsList.SelectedItem = value; }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Selected == null)
            {
                MessageBox.Show("Select configuration", "Not selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            SettingsWindow window = new SettingsWindow();
            var selectedItem = Selected;
            ModelSettings settingsCopy = Selected.MakeCopy();

            window.DataContext = selectedItem;
            window.Owner = this;
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                _fileHelper.SaveSetting(selectedItem);
            }
            else
            {
                bool isActiveConfig = MainViewModel.ActiveConfigutaion == selectedItem;
                int currentIdx = Items.IndexOf(selectedItem);
                Items[currentIdx] = settingsCopy;
                Selected = settingsCopy;

                if (isActiveConfig)
                    MainViewModel.ActiveConfigutaion = settingsCopy;
            }

        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            ModelSettings model = new ModelSettings() { Name = "Data File Settings" };
            SettingsWindow window = new SettingsWindow();
            window.DataContext = model;
            window.Owner = this;
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                _fileHelper.SaveSetting(model);
                Items.Add(model);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Selected == null)
            {
                MessageBox.Show("Select configuration", "Not selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to delete this configuration?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var selectedItem = Selected;
                _fileHelper.DeleteSetting(selectedItem);
                if (MainViewModel.ActiveConfigutaion == selectedItem)
                    MainViewModel.ActiveConfigutaion = null;

                Items.Remove(selectedItem);
                Selected = null;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public ModelSettings InitalSelected { get; set; }
    }
}
