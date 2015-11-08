using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings.Fields
{
  
    public class WorkDateField : BaseField
    {
        private string _dateFormat;
        public string DateFormat
        {
            get { return _dateFormat; }
            set { _dateFormat = value; RaisePropertyChanged("DateFormat"); }
        }
    }
}
