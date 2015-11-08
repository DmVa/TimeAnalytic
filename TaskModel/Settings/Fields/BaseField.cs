using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings.Fields
{
   
    public class BaseField : BasePropertyChanged
    {
        private string _internalName;
        private int _position;
        private string _stringValue;
        public int Position
        {
            get { return _position; }
            set { _position = value; RaisePropertyChanged("Position"); }
        }
        
        public string StringValue
        {
            get { return _stringValue; }
            set { _stringValue = value; RaisePropertyChanged("StringValue"); }
        }
        public string InternalName
        {
            get { return _internalName; }
            set { _internalName = value; RaisePropertyChanged("InternalName"); }
        }
    }
}
