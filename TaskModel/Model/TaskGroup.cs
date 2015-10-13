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

        private double _totalEstimationDevelopment;
        private double _totalLeftOnBeginig; // not used
        private double _totalTimeBooked;
        private double _totalDoneBookedDevelopment;
        private double _totalDoneEstimationDevelopment;

        private double _totalBookedMeetings;
        private double _totalBookedDevelopment;

        private double _totalSpentManagedByDeveloper; // not used
        private double _totalSpentNotManagedByDeveloper; // not used
        private double _rateDoneBookedToBookedDevelopment;
        private double _rateDoneEstimationToDevelopment;

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

        public double TotalEstimationDevelopment
        {
            get { return _totalEstimationDevelopment; }
            set { _totalEstimationDevelopment = value; RaisePropertyChanged("TotalEstimationDevelopment"); }
        }

        public double TotalLeftOnBeginig
        {
            get { return _totalLeftOnBeginig; }
            set { _totalLeftOnBeginig = value; RaisePropertyChanged("TotalLeftOnBeginig"); }
        }

        public double TotalTimeBooked
        {
            get { return _totalTimeBooked; }
            set { _totalTimeBooked = value; RaisePropertyChanged("TotalTimeBooked"); }
        }

        public double TotalDoneBookedDevelopment
        {
            get { return _totalDoneBookedDevelopment; }
            set { _totalDoneBookedDevelopment = value; RaisePropertyChanged("TotalDoneBookedDevelopment"); }
        }

        public double TotalDoneEstimationDevelopment
        {
            get { return _totalDoneEstimationDevelopment; }
            set { _totalDoneEstimationDevelopment = value; RaisePropertyChanged("TotalDoneEstimationDevelopment"); }
        }

        public double TotalBookedMeetings
        {
            get { return _totalBookedMeetings; }
            set { _totalBookedMeetings = value; RaisePropertyChanged("TotalBookedMeetings"); }
        }
        public double TotalBookedDevelopment
        {
            get { return _totalBookedDevelopment; }
            set { _totalBookedDevelopment = value; RaisePropertyChanged("TotalBookedDevelopment"); }
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

        public double RateDoneBookedToBookedDevelopment
        {
            get { return _rateDoneBookedToBookedDevelopment; }
            set { _rateDoneBookedToBookedDevelopment = value; RaisePropertyChanged("RateDoneBookedToBookedDevelopment"); }
        }
        public double RateDoneEstimationToDevelopment
        {
            get { return _rateDoneEstimationToDevelopment; }
            set { _rateDoneEstimationToDevelopment = value; RaisePropertyChanged("RateDoneEstimationToDevelopment"); }
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
            _totalEstimationDevelopment = _tasks.Where( x => x.IsTaskRelatesToDevelopment).Sum(x => x.Estimation);

            //_totalLeftOnBeginig = _tasks.Sum(x => x.LeftOnBegining);//not used

            _totalTimeBooked = _tasks.Sum(x => x.TimeSpentByDev);

            _totalDoneBookedDevelopment = _tasks.Where( x => x.IsDone && x.IsTaskRelatesToDevelopment).Sum(x => x.TimeSpentByDev);
            _totalDoneEstimationDevelopment = _tasks.Where( x => x.IsDone && x.IsTaskRelatesToDevelopment).Sum(x => x.Estimation);

            _totalBookedMeetings = _tasks.Where(x => x.IsTaskRelatesToMettings).Sum(x => x.TimeSpentByDev);
            _totalBookedDevelopment =  _tasks.Where(x => x.IsTaskRelatesToDevelopment).Sum(x => x.TimeSpentByDev);
            
            //_totalSpentManagedByDeveloper = _tasks.Where(x => x.IsTaskManagedByDeveloper).Sum(x => x.TimeSpentByDev); //not used
            //_totalSpentNotManagedByDeveloper = _tasks.Where(x => !x.IsTaskManagedByDeveloper).Sum(x => x.TimeSpentByDev);//not used.

            if (_totalBookedDevelopment > 0)
            {
                _rateDoneBookedToBookedDevelopment = _totalDoneBookedDevelopment / _totalBookedDevelopment;
            }
            else
            {
                if (_totalDoneBookedDevelopment > 0)
                    _rateDoneBookedToBookedDevelopment = 1;
                else
                    _rateDoneBookedToBookedDevelopment = 0;
            }

            if (_totalDoneEstimationDevelopment > 0)
            {
                _rateDoneEstimationToDevelopment = _totalDoneEstimationDevelopment / _totalBookedDevelopment;
            }
            else
            {
                if (_totalDoneEstimationDevelopment > 0)
                    _rateDoneEstimationToDevelopment = 1;
                else
                    _rateDoneEstimationToDevelopment = 0;
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
              _totalEstimationDevelopment = 0;
              _totalLeftOnBeginig = 0; //not used
              _totalTimeBooked = 0;
              _totalDoneBookedDevelopment = 0;
              _totalDoneEstimationDevelopment = 0;
              _totalBookedMeetings = 0;
              _totalBookedDevelopment = 0;
              _totalSpentManagedByDeveloper = 0; //not used
              _totalSpentNotManagedByDeveloper = 0; //not used
              _rateDoneBookedToBookedDevelopment = 0;
              _rateDoneEstimationToDevelopment = 0;
              _totalUnderEstimate = 0;
        }
#endregion

    }
}
