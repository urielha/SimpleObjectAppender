using log4net.Core;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace SimpleObjectAppender
{
    interface IDescriptor
    {
        List<KeyValuePair<string, string>> getAllProperties(object msgObj);
        string getPropertyValue(object msgObj, string propertyName);
    }

    public class Descriptor : IDescriptor
    {
        private static BindingFlags flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private List<string> properties = new List<string>();

        public void AddProperty(string propName)
        {
            properties.Add(propName);
        }

        public List<KeyValuePair<string, string>> getAllProperties(object msgObj)
        {
            List<KeyValuePair<string, string>> kvpList = new List<KeyValuePair<string, string>>();
            foreach (var name in properties)
            {
                var value = getPropertyValue(msgObj, name);
                if (value != null)
                {
                    kvpList.Add(new KeyValuePair<string, string>(name, value));
                }
            }
            return kvpList;
        }

        public string getPropertyValue(object msgObj, string propertyName)
        {
            var propInfo = msgObj.GetType().GetProperty(propertyName, flags);
            if (propInfo == null)
            {
                return null;
            }

            var value = propInfo.GetValue(msgObj, null).ToString();
            return value;
        }
    }
}