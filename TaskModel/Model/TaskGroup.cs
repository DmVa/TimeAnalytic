using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TaskModel.Model
{
    public class TaskGroup : BasePropertyChanged
    {
        private string _id;
        private bool _isSummary;
        private string _title;

        private double _estimation;
        private double _totalLeftOnBeginig;
        private double _totalTimeSpentByDev;
        private double _totalDone;
        private double _totalRelatesToMettings;
        private double _totalRelatesToDevelopment;
        private double _totalSpentManagedByDeveloper;
        private double _totalSpentNotManagedByDeveloper;
        private double _rateDileveredToDevelopment;
        private double _totalUnderEstimate;
        private ObservableCollection<Task> _tasks;

        public TaskGroup()
        {
            _tasks = new ObservableCollection<Task>();    
        }

#region "Public properties"
        public string Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("Id"); }
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

        public double TotalLeftOnBeginig
        {
            get { return _totalLeftOnBeginig; }
            set { _totalLeftOnBeginig = value; RaisePropertyChanged("TotalLeftOnBeginig"); }
        }

        public double TotalTimeSpentByDev
        {
            get { return _totalTimeSpentByDev; }
            set { _totalTimeSpentByDev = value; RaisePropertyChanged("TotalTimeSpentByDev"); }
        }

        public double TotalDone
        {
            get { return _totalDone; }
            set { _totalDone = value; RaisePropertyChanged("TotalDone"); }
        }

        public double TotalRelatesToMettings
        {
            get { return _totalRelatesToMettings; }
            set { _totalRelatesToMettings = value; RaisePropertyChanged("TotalRelatesToMettings"); }
        }
        public double TotalRelatesToDevelopment
        {
            get { return _totalRelatesToDevelopment; }
            set { _totalRelatesToDevelopment = value; RaisePropertyChanged("TotalRelatesToDevelopment"); }
        }

        public double TotalSpentManagedByDeveloper
        {
            get { return _totalSpentManagedByDeveloper; }
            set { _totalSpentManagedByDeveloper = value; RaisePropertyChanged("TotalSpentManagedByDeveloper"); }
        }

        public double TotalSpentNotManagedByDeveloper
        {
            get { return _totalSpentNotManagedByDeveloper; }
            set { _totalSpentNotManagedByDeveloper = value; RaisePropertyChanged("TotalSpentNotManagedByDeveloper"); }
        }
        public double TotalUnderEstimate
        {
            get { return _totalUnderEstimate; }
            set { _totalUnderEstimate = value; RaisePropertyChanged("TotalUnderEstimate"); }
        }

        public double RateDileveredToDevelopment
        {
            get { return _rateDileveredToDevelopment; }
            set { _rateDileveredToDevelopment = value; RaisePropertyChanged("RateDileveredToDevelopment"); }
        }
        public bool IsSummary
        {
            get { return _isSummary; }
            set { _isSummary = value; RaisePropertyChanged("IsSummary"); }
        }
        public  ObservableCollection<Task> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; RaisePropertyChanged("Tasks"); }
        }
#endregion
#region "Methods"

        public void CalculateTotals()
        {
            CalcItemsTotals();
            ResetTotals();
            _estimation = _tasks.Sum(x => x.Estimation);
            _totalLeftOnBeginig = _tasks.Sum(x => x.LeftOnBegining);
            _totalTimeSpentByDev = _tasks.Sum(x => x.TimeSpentByDev);
            _totalDone = _tasks.Where( x => x.IsDone).Sum(x => x.TimeSpentByDev);
            _totalRelatesToMettings = _tasks.Where(x => x.IsTaskRelatesToMettings).Sum(x => x.TimeSpentByDev);
            _totalRelatesToDevelopment =  _tasks.Where(x => x.IsTaskRelatesToDevelopment).Sum(x => x.TimeSpentByDev);
            _totalSpentManagedByDeveloper = _tasks.Where(x => x.IsTaskManagedByDeveloper).Sum(x => x.TimeSpentByDev); 
            _totalSpentNotManagedByDeveloper = _tasks.Where(x => !x.IsTaskManagedByDeveloper).Sum(x => x.TimeSpentByDev);
            if (_totalRelatesToDevelopment > 0)
            {
                _rateDileveredToDevelopment = _totalDone / _totalRelatesToDevelopment;
            }
            else
            {
                if (_totalDone > 0)
                    _totalRelatesToDevelopment = 1;
            }

            _totalUnderEstimate = _tasks.Where(x => x.IsTaskRelatesToDevelopment).Sum(x => x.UnderEstimate); 

            RaisePropertyChanged(string.Empty);
        }

        private void CalcItemsTotals()
        {
           foreach (var task in Tasks)
           {
               task.CalcCalulatedValues();
           }
        }

        private void ResetTotals()
        {
              _estimation = 0;
              _totalLeftOnBeginig = 0;
              _totalTimeSpentByDev = 0;
              _totalDone = 0;
              _totalRelatesToMettings = 0;
              _totalRelatesToDevelopment = 0;
              _totalSpentManagedByDeveloper = 0;
              _totalSpentNotManagedByDeveloper = 0;
              _rateDileveredToDevelopment = 0;
        }
#endregion

    }
}
