using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskModel.Settings
{
    public class SettingsFactory
    {
        public static ModelSettings CreateJiraTempoSettings()
        {
            ModelSettings settings = new ModelSettings();
            settings.Name = "Jira";

            return new ModelSettings();
        }
    }
}
