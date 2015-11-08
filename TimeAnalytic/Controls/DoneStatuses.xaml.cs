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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskModel.Settings;

namespace TimeAnalytic.Controls
{
    /// <summary>
    /// Interaction logic for DoneStatuses.xaml
    /// </summary>
    public partial class DoneStatuses : UserControl
    {
        public DoneStatuses()
        {
            InitializeComponent();
        }

        public Collection<DoneStatus> Items
        {
            get { return ((ModelSettings)DataContext).DoneStatuses; }
            set { DataContext = value; }
        }

        public DoneStatus Selected
        {
            get { return ItemsList.SelectedItem as DoneStatus; }
            set { ItemsList.SelectedItem = value; }
        }

     

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Selected == null)
            {
                MessageBox.Show("Select line", "Not selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to delete this line?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var selected = Selected;
                Selected = null;
                Items.Remove(selected);
            }
        }
    }
}
