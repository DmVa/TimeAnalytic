using System;
using System.Configuration;

namespace TaskModel.Configuraion
{
    public class SpecialTaskConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("Key", DefaultValue = "", IsRequired = true)]
        public string Key
        {
            get { return (string)this["Key"]; }
            set { this["Key"] = value; }
        }

        [ConfigurationProperty("Description", DefaultValue = "", IsRequired = false)]
        public string Description
        {
            get { return (string)this["Description"]; }
            set { this["Description"] = value; }
        }
    }
}

