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
        private string _versionString;
        private ApplicationSettings _applicationSettings;

        private ObservableCollection<TaskGroup> _tasksGroups;
        private ObservableCollection<ModelSettings> _configurations;
        private ModelSettings _activeConfiguration;

       
        private FileHelper _fileHelper;
        private DataLoadManager _dataLoader;

        public MainWindowViewModel()
        {
            _fileHelper = new FileHelper();
            _dataLoader = new DataLoadManager();
            Init();
            InitTasksGroups();
        }

        private void Init()
        {
            CalcVersionString();
            _fileHelper.MoveDeployedRecources();
            _applicationSettings = _fileHelper.LoadAppSettings();
            if (_applicationSettings == null)
                _applicationSettings = ApplicationSettings.CreateDefault();

            _configurations = new ObservableCollection<ModelSettings>();
            IEnumerable<ModelSettings> loaded = _fileHelper.LoadAllSettings();
            foreach (var setting in loaded)
            {
                _configurations.Add(setting);
            }

            ModelSettings active = loaded.FirstOrDefault(x=>x.FileName == _applicationSettings.ConfigurationFile);
            if (active != null)
                ActiveConfigutaion = active;
        }

        private void CalcVersionString()
        {
           Version theVersion = AppDomain.CurrentDomain.DomainManager.EntryAssembly.GetName().Version;
            _versionString = string.Format("ver: {0}.{1}.{2}.{3}", theVersion.Major, theVersion.Minor,
                theVersion.Build, theVersion.Revision);
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


        public ObservableCollection<ModelSettings> Configurations
        {
            get { return _configurations; }
            set { _configurations = value; RaisePropertyChanged("Configurations"); }
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

        public string VersionString
        {
            get { return _versionString; }
            set { _versionString = value; RaisePropertyChanged("VersionString"); }
        }

#endregion

        public void LoadData()
        {
            TasksGroups = _dataLoader.LoadTimeSheetReport(_applicationSettings.DataFile, _applicationSettings.DateFrom, _applicationSettings.DateTo, _activeConfiguration); 
        }

        private void InitTasksGroups()
        {
            if (!string.IsNullOrEmpty(_applicationSettings.DataFile) && _activeConfiguration != null)
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

        public void SaveApplicationConfiguration()
        {
            if (_activeConfiguration != null)
                _applicationSettings.ConfigurationFile = _activeConfiguration.FileName;

            _fileHelper.SaveAppSettings(_applicationSettings);
        }
    }
}
