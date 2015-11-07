using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string WorkSheetName
        {
            get { return _workSheetName; }
            set { _workSheetName = value; RaisePropertyChanged("WorkSheetName"); }
        }

        public string PrefixToUrl
        {
            get { return _prefixToUrl; }
            set { _prefixToUrl = value; RaisePropertyChanged("PrefixToUrl"); }
        }
        public string DateFormat
        {
            get { return _dateFormat; }
            set { _dateFormat = value; RaisePropertyChanged("DateFormat"); }
        }
        public int KeyPosition
        {
            get { return _keyPosition; }
            set { _keyPosition = value; RaisePropertyChanged("KeyPosition"); }
        }
        public int SummaryPosition
        {
            get { return _summaryPosition; }
            set { _summaryPosition = value; RaisePropertyChanged("SummaryPosition"); }
        }
        public int HoursPosition
        {
            get { return _hoursPosition; }
            set { _hoursPosition = value; RaisePropertyChanged("HoursPosition"); }
        }
        public int WorkDatePosition
        {
            get { return _workDatePosition; }
            set { _workDatePosition = value; RaisePropertyChanged("WorkDatePosition"); }
        }
        public int UserKeyPosition
        {
            get { return _userKeyPosition; }
            set { _userKeyPosition = value; RaisePropertyChanged("UserKeyPosition"); }
        }
        public int UserFullNamePosition
        {
            get { return _userFullNamePosition; }
            set { _userFullNamePosition = value; RaisePropertyChanged("UserFullNamePosition"); }
        }
        public int IssueStatusPosition
        {
            get { return _issueStatusPosition; }
            set { _issueStatusPosition = value; RaisePropertyChanged("IssueStatusPosition"); }
        }
        public int IssueOriginalEstimatePosition
        {
            get { return _issueOriginalEstimatePosition; }
            set { _issueOriginalEstimatePosition = value; RaisePropertyChanged("IssueOriginalEstimatePosition"); }
        }
    }
}
