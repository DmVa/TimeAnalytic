using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TaskModel.DataLoad;
using TaskModel.Model;
using TaskModel.Settings;

namespace TaskModel.ViewModel
{
    public class MainWindowViewModel : BasePropertyChanged
    {
        private ApplicationSettings _applicationSettings;

        private ObservableCollection<TaskGroup> _tasksGroups;
        private ObservableCollection<ModelSettings> _configurations;
        private ModelSettings _activeConfiguration;

        private DateTime _dateFrom;
        private DateTime _dateTo;
        private string _dataFile;
        private FileHelper _fileHelper;
        private DataLoadManager _dataLoader;

        public MainWindowViewModel()
        {
            _applicationSettings = new ApplicationSettings();
            _configurations = new ObservableCollection<ModelSettings>();
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

        public ModelSettings ActiveConfigutaion 
        {
            get { return _activeConfiguration; }
            set { _activeConfiguration = value; RaisePropertyChanged("ActiveConfigutaion"); }
        }


        public ObservableCollection<ModelSettings> Cofigurations
        {
            get { return _configurations; }
            set { _configurations = value; RaisePropertyChanged("Cofigurations"); }
        }

        public string DataFile
        {
            get { return _applicationSettings.DataFile; }
            set { _applicationSettings.DataFile = value; RaisePropertyChanged("DataFile"); }
        }

        public DateTime DateFrom
        {
            get { return _applicationSettings.DateFrom; }
            set { _applicationSettings.DateFrom = value; RaisePropertyChanged("DateFrom"); }
        }

        public DateTime DateTo
        {
            get { return _applicationSettings.DateTo; }
            set { _applicationSettings.DateTo = value; RaisePropertyChanged("DateTo"); }
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
