using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TaskModel.DataLoad;
using TaskModel.Model;

namespace TaskModel.ViewModel
{
    public class MainWindowViewModel : BasePropertyChanged
    {
        private ObservableCollection<TaskGroup> _tasksGroups;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private string _dataFile;
        private FileHelper _fileHelper;
        private DataLoadManager _dataLoader;

        public MainWindowViewModel()
        {
            _fileHelper = new FileHelper();
            _dataLoader = new DataLoadManager();
            _dataFile = _fileHelper.GetLastModifiedFile();
            _dateTo = DateTime.Now.Date;
            _dateFrom = _dateTo.AddDays(-7);
            InitTasksGroups();
        
        }
#region "Public properties"
        public ObservableCollection<TaskGroup> TasksGroups
        {
            get { return _tasksGroups; }
            set { _tasksGroups = value; RaisePropertyChanged("TasksGroups"); }
        }

        public string DataFile
        {
            get { return _dataFile; }
            set { _dataFile = value; RaisePropertyChanged("DataFile"); }
        }
        public DateTime DateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; RaisePropertyChanged("DateFrom"); }
        }
        public DateTime DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; RaisePropertyChanged("DateTo"); }
        }

#endregion

        public void LoadData()
        {
            TasksGroups = _dataLoader.LoadTimeSheetReport(_dataFile, _dateFrom, _dateTo); 
        }

        private void InitTasksGroups()
        {
            if (!string.IsNullOrEmpty(_dataFile))
            {
                try
                {
                    LoadData();
                }
                catch { }
                //ignores

            }

            //if (_tasksGroups == null)
            //    _tasksGroups = TestHelper.CreateTestData();
        }


        public string ExportData(string fileName)
        {
            string error = null;
            try
            {
                DataExport export = new DataExport();
                export.Export(fileName, DateFrom, DateTo, TasksGroups);
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return error;
            
        }
    }
}
