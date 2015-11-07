using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TaskModel.Settings
{
    public class ModelSettings : BasePropertyChanged
    {
        private ObservableCollection<string> _doneStatuses;
        private ObservableCollection<SpecialTask> _meetingTasks;
        private SourceFileSettings _sourceFileSettings;
        private string _name;
        private string _fileName;

        public ModelSettings()
        {
            Init();
        }

        private void Init()
        {
            _doneStatuses = new ObservableCollection<string>();
            _meetingTasks = new ObservableCollection<SpecialTask>();
            _sourceFileSettings = new SourceFileSettings();
            _name = string.Empty;
        }

        public ObservableCollection<string> DoneStatuses
        {
            get {  return _doneStatuses;}
            set {  _doneStatuses = value; RaisePropertyChanged("DoneStatusNames"); }
        }

        public ObservableCollection<SpecialTask> MeetingTasks
        {
            get { return _meetingTasks; }
            set { _meetingTasks = value; RaisePropertyChanged("MeetingTasks"); }
        }

        public SourceFileSettings SourceFileSettings
        {
            get { return _sourceFileSettings; }
            set { _sourceFileSettings = value; RaisePropertyChanged("SourceFileSettings"); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; RaisePropertyChanged("FileName"); }
        }
    }
}
