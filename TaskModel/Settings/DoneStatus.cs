using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings
{
  
    public class DoneStatus :  BasePropertyChanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }
    }
}
