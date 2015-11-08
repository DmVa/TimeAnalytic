using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskModel.Settings.Fields;

namespace TaskModel.Settings
{
   
    public class SourceFileSettings : BasePropertyChanged
    {
        private string _workSheetName;
        private string _prefixToUrl;
        private string _dateFormat;

        private int _keyPosition;
        private int _summaryPosition;
        private int _hoursPosition;
        private int _workDatePosition;
        private int _userKeyPosition;
        private int _userFullNamePosition;
        private int _issueStatusPosition;
        private int _issueOriginalEstimatePosition;

        private BaseField _keyField;
        private BaseField _titleField;
        private AssigneeField _assigneeField;
        private EstimationField _estimationField;
        private BaseField _statusField;
        private WorkDateField _dateField;
        private BaseField _userNameField;
        private BaseField _timeSpentField;
        private UrlField _urlField;

        public SourceFileSettings()
        {
            Init();
        }

        private void Init()
        {
            _keyField = new BaseField() {InternalName="Key", Position=3};
            _titleField = new BaseField() { InternalName = "Title", Position = 4 };
            _assigneeField = new AssigneeField() { InternalName = "Assignee", Position = 5 };
            _estimationField = new EstimationField() { InternalName = "Estimation", Position = 6 };
            _statusField = new BaseField() { InternalName = "Status", Position = 7 };
            _dateField = new WorkDateField() { InternalName = "Work Date", Position = 9,  DateFormat="dd.MM.yyyy" };
            _urlField = new UrlField() { InternalName = "Url", Position = 3, PrefixToValue = "http://" };
            _userNameField = new BaseField() { InternalName = "User Name", Position = 10};
            _timeSpentField = new BaseField() { InternalName = "Time Spent", Position = 11 };
        }

        
        public BaseField KeyField
        {
            get { return _keyField; }
            set { _keyField = value; RaisePropertyChanged("KeyField"); }
        }
        public BaseField TitleField
        {
            get { return _titleField; }
            set { _titleField = value; RaisePropertyChanged("TitleField"); }
        }
        public AssigneeField AssigneeField
        {
            get { return _assigneeField; }
            set { _assigneeField = value; RaisePropertyChanged("AssigneeField"); }
        }

        public EstimationField EstimationField
        {
            get { return _estimationField; }
            set { _estimationField = value; RaisePropertyChanged("EstimationField"); }
        }
        public BaseField StatusField
        {
            get { return _statusField; }
            set { _statusField = value; RaisePropertyChanged("StatusField"); }
        }
        public WorkDateField DateField
        {
            get { return _dateField; }
            set { _dateField = value; RaisePropertyChanged("DateField"); }
        }
        public BaseField UserNameField
        {
            get { return _userNameField; }
            set { _userNameField = value; RaisePropertyChanged("UserNameField"); }
        }
        public BaseField TimeSpentField
        {
            get { return _timeSpentField; }
            set { _timeSpentField = value; RaisePropertyChanged("TimeSpentField"); }
        }

        public UrlField UrlField
        {
            get { return _urlField; }
            set { _urlField = value; RaisePropertyChanged("UrlField"); }
        }
    }
}
