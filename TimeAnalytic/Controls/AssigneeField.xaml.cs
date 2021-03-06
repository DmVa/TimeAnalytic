﻿using System;
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
    /// Interaction logic for AssigneeField.xaml
    /// </summary>
    public partial class AssigneeField : UserControl
    {
        public AssigneeField()
        {
            InitializeComponent();
        }

        public Collection<ValueMapping> Items
        {
            get { return ((TaskModel.Settings.Fields.AssigneeField)DataContext).NameMapping; }
            set { DataContext = value; }
        }

        public ValueMapping Selected
        {
            get { return MappingList.SelectedItem as ValueMapping; }
            set { MappingList.SelectedItem = value; }
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
