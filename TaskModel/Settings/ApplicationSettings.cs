using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings
{
    public class ApplicationSettings : BasePropertyChanged
    {
        private string _dataFile;
        private string _configurationFile;
        private DateTime _dateFrom;
        private DateTime _dateTo;

        public string DataFile
        {
            get { return _dataFile; }
            set { _dataFile = value; RaisePropertyChanged("DataFile"); }
        }

        public string ConfigurationFile
        {
            get { return _configurationFile; }
            set { _configurationFile = value; RaisePropertyChanged("ConfigurationFile"); }
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

        internal static ApplicationSettings CreateDefault()
        {
            ApplicationSettings settings = new ApplicationSettings();
            settings._dateTo = DateTime.Now.Date;
            settings._dateFrom = settings._dateTo.AddDays(-6);
            return settings;
        }
    }
}
