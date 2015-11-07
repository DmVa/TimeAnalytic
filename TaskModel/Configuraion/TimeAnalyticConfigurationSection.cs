using System;
using System.Configuration;

namespace TaskModel.Configuraion
{
    public class TimeAnalyticConfigurationSection : ConfigurationSection
    {
        public const string SECTION_NAME = "TimeAnalytic";

        [ConfigurationProperty("Tasks")]
        public TaskCollection Tasks
        {
            get { return base["Tasks"] as TaskCollection; }
        }


        [ConfigurationProperty("DoneStatuses")]
        public DoneStatusCollection DoneStatuses
        {
            get { return base["DoneStatuses"] as DoneStatusCollection; }
        }

        [ConfigurationProperty("UrlPrefixToKey")]
        public string UrlPrefixToKey
        {
            get { return base["UrlPrefixToKey"] as string; }
        }

        
    }
}
