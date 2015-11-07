using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TaskModel.Configuraion
{
    [ConfigurationCollection(typeof(SpecialTaskConfigurationElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class TaskCollection : ConfigurationElementCollection
    {
        public SpecialTaskConfigurationElement this[int index]
        {
            get { return (SpecialTaskConfigurationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(SpecialTaskConfigurationElement serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SpecialTaskConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SpecialTaskConfigurationElement)element).Key;
        }

        public void Remove(SpecialTaskConfigurationElement serviceConfig)
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
