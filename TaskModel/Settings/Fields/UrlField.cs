using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings.Fields
{
   
    public class UrlField: BaseField
    {
        private string _prefixToValue;
        public string  PrefixToValue
        {
            get { return _prefixToValue; }
            set { _prefixToValue = value; RaisePropertyChanged("PrefixToValue"); }
        }
    }
}
