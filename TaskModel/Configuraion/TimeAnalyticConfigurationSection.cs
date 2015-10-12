using System;
using System.Configuration;

namespace TaskModel.Configuraion
{
    public class TimeAnalyticConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("Tasks")]
        public TaskCollection Tasks
        {
            get { return base["Tasks"] as TaskCollection; }
        }

        [ConfigurationProperty("DoneStatuses")]
        public TaskCollection DoneStatuses
        {
            get { return base["DoneStatuses"] as TaskCollection; }
        }
    }
}
