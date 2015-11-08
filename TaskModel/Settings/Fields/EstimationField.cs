using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings.Fields
{
  
    public class EstimationField : BaseField
    {
        private bool _isUseStoryPointsFromMainPosition;
        private double  _storyPointCost;
        private int _alternatePosition;

        public int AlternatePosition
        {
            get { return _alternatePosition; }
            set { _alternatePosition = value; RaisePropertyChanged("AlternatePosition"); }
        }

        public bool IsUseStoryPointsFromMainPosition
        {
            get { return _isUseStoryPointsFromMainPosition; }
            set { _isUseStoryPointsFromMainPosition = value; RaisePropertyChanged("IsUseStoryPointsFromMainPosition"); }
        }

        public double StoryPointCost
        {
            get { return _storyPointCost; }
            set { _storyPointCost = value; RaisePropertyChanged("StoryPointCost"); }
        }

    }
}
