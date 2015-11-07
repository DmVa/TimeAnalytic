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
using System.Windows.Shapes;
using TaskModel.Settings;

namespace TimeAnalytic
{
    /// <summary>
    /// Interaction logic for SettingsList.xaml
    /// </summary>
    public partial class SettingsListWindow : Window
    {
        public SettingsListWindow()
        {
            InitializeComponent();
        }

        public ModelSettings Selected
        {
            get { return SettingsList.SelectedItem as ModelSettings; }
            set { SettingsList.SelectedItem = value; }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            // 
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
                // do delete.
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
