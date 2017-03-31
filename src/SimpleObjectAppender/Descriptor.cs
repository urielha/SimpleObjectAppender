using log4net.Core;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace SimpleObjectAppender
{
    interface IDescriptor
    {
        string getPropertyValue(object msgObj, string propertyName);
    }

    public class Descriptor : IDescriptor
    {
        private static readonly BindingFlags flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

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