using System;
using System.Configuration;

namespace TaskModel.Configuraion
{
    public class TaskConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("Key", DefaultValue = "", IsRequired = true)]
        public string Key
        {
            get { return (string)this["Key"]; }
            set { this["Key"] = value; }
        }

        [ConfigurationProperty("IsMeeting", DefaultValue = false, IsRequired = false)]
        public bool IsMeeting
        {
            get { return (bool)this["IsMeeting"]; }
            set { this["IsMeeting"] = value; }
        }

        [ConfigurationProperty("Description", DefaultValue = "", IsRequired = false)]
        public string Description
        {
            get { return (string)this["Description"]; }
            set { this["Description"] = value; }
        }
    }
}

