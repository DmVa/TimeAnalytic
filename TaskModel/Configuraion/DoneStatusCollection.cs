using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TaskModel.Configuraion
{
   
    [ConfigurationCollection(typeof(DoneStatusConfigurationElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class DoneStatusCollection : ConfigurationElementCollection
    {
        public DoneStatusConfigurationElement this[int index]
        {
            get { return (DoneStatusConfigurationElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void Add(DoneStatusConfigurationElement serviceConfig)
        {
            BaseAdd(serviceConfig);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DoneStatusConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DoneStatusConfigurationElement)element).Status;
        }

        public void Remove(DoneStatusConfigurationElement serviceConfig)
        {
            BaseRemove(serviceConfig.Status);
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
