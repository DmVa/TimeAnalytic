using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TaskModel.Model;

namespace TaskModel.ViewModel
{
    public class MainWindowViewModel : BasePropertyChanged
    {
        private ObservableCollection<TaskGroup> _tasksGroups;

        public MainWindowViewModel()
        {
            _tasksGroups = TestHelper.CreateTestData();
        }

        public ObservableCollection<TaskGroup> TasksGroups
        {
            get { return _tasksGroups; }
            set { _tasksGroups = value; RaisePropertyChanged("TasksGroups"); }
        }
    }
}
