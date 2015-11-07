using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings.Fields
{
    public class EstimationField : BaseField
    {
        private bool _isPreferStoryPoints;
        private double  _storyPointCost;
        private int _alternatePosition;

        public int AlternatePosition
        {
            get { return _alternatePosition; }
            set { _alternatePosition = value; RaisePropertyChanged("AlternatePosition"); }
        }

        public bool IsPreferStoryPoints
        {
            get { return _isPreferStoryPoints; }
            set { _isPreferStoryPoints = value; RaisePropertyChanged("IsPreferStoryPoints"); }
        }

        public double StoryPointCost
        {
            get { return _storyPointCost; }
            set { _storyPointCost = value; RaisePropertyChanged("StoryPointCost"); }
        }

    }
}
