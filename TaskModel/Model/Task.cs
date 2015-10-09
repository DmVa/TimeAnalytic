﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Model
{
    public class Task : BasePropertyChanged
    {
        private string _key;
        private string _keyName;
        private string _url;
        private string _title;
        private double _estimation;
        private double _leftOnBeginig;
        private double _timeSpentByDev;
        private bool _isDone;
        private bool _isTaskRelatesToMettings;
        private bool _isTaskRelatesToDevelopment;
        private bool _isTaskManagedByDeveloper;

        public string Key
        {
            get { return _key; }
            set { _key = value; RaisePropertyChanged("Key"); }
        }

        public string KeyName
        {
            get { return _keyName; }
            set { _keyName = value; RaisePropertyChanged("KeyName"); }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; RaisePropertyChanged("Url"); }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; RaisePropertyChanged("Title"); }
        }

        public double Estimation
        {
            get { return _estimation; }
            set { _estimation = value; RaisePropertyChanged("Estimation"); } 
        }

        public double LeftOnBegining
        {
            get { return _leftOnBeginig; }
            set { _leftOnBeginig = value; RaisePropertyChanged("LeftOnBegining"); }  
        }

        public double TimeSpentByDev
        {
            get { return _timeSpentByDev; }
            set { _timeSpentByDev = value; RaisePropertyChanged("TimeSpentByDev"); }  
        }

        public bool IsDone
        {
            get { return _isDone; }
            set { _isDone = value; RaisePropertyChanged("IsDone"); }
        }

        public bool IsTaskRelatesToMettings
        {
            get { return _isTaskRelatesToMettings; }
            set { _isTaskRelatesToMettings = value; RaisePropertyChanged("IsTaskRelatesToMettings"); }
        }
        public bool IsTaskRelatesToDevelopment
        {
            get { return _isTaskRelatesToDevelopment; }
            set { _isTaskRelatesToDevelopment = value; RaisePropertyChanged("IsTaskRelatesToDevelopment"); }
        }
        public bool IsTaskManagedByDeveloper
        {
            get { return _isTaskManagedByDeveloper; }
            set { _isTaskManagedByDeveloper = value; RaisePropertyChanged("IsTaskManagedByDeveloper"); }
        }
    }
}
