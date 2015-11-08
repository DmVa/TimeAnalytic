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
            InitializeComponent();
        }

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
            window.DataContext = selectedItem;
            window.Owner = this;
            window.ShowDialog();
            if (window.DialogResult == true)
            {
                _fileHelper.SaveSetting(selectedItem);
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
                Items.Remove(selectedItem);
                Selected = null;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
