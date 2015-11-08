using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings
{
 
    public class SpecialTask : BasePropertyChanged
    {
        private string _key;
        private string _description;

        public string Key
        {
            get { return _key; }
            set { _key = value; RaisePropertyChanged("Key"); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged("Description"); }
        }
    }
}
