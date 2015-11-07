using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings
{
    public class ValueMapping : BasePropertyChanged
    {
        private string _source;
        private string _target;

        public string Source
        {
            get { return _source; }
            set { _source = value; RaisePropertyChanged("Source"); }
        }

        public string Target
        {
            get { return _target; }
            set { _target = value; RaisePropertyChanged("Target"); }
        }
    }
}
