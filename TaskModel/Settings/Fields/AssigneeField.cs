using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TaskModel.Settings.Fields
{

    public class AssigneeField : BaseField
    {
        private ObservableCollection<ValueMapping> _nameMapping;

        public AssigneeField()
        {
            _nameMapping = new ObservableCollection<ValueMapping>();
        }

        public ObservableCollection<ValueMapping> NameMapping
        {
            get { return _nameMapping; }
            set { _nameMapping = value; RaisePropertyChanged("NameMapping"); }
        }
    }
}
