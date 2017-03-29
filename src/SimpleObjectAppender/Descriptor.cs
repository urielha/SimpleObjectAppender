using log4net.Core;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleObjectAppender
{
    interface IDescriptor
    {
        List<KeyValuePair<string, string>> getAllProperties(object msgObj);
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
            List<KeyValuePair<string, string>> kvp = new List<KeyValuePair<string, string>>();
            foreach (var name in properties)
            {
                var propInfo = msgObj.GetType().GetProperty(name, flags);
                if(propInfo == null)
                {
                    continue;
                }

                var value = propInfo.GetValue(msgObj, null).ToString();
                kvp.Add(new KeyValuePair<string, string>(name, value));
            }
            return kvp;
        }
    }
}