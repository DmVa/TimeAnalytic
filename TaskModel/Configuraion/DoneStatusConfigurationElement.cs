using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TaskModel.Configuraion
{
    public class DoneStatusConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("Status", DefaultValue = "", IsRequired = true)]
        public string Status
        {
            get { return (string)this["Status"]; }
            set { this["Status"] = value; }
        }
    }
}
