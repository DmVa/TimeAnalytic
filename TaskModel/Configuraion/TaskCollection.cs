using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TaskModel.Configuraion
{
    [ConfigurationCollection(typeof(TaskConfigurationElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class TaskCollection : ConfigurationElementCollection
    {
        public TaskConfigurationElement this[int index]
        {
            get { return (TaskConfigurationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(TaskConfigurationElement serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TaskConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TaskConfigurationElement)element).Key;
        }

        public void Remove(TaskConfigurationElement serviceConfig)
        {
            BaseRemove(serviceConfig.Key);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(String name)
        {
            BaseRemove(name);
        }

    }
}
