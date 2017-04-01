using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleObjectAppender.Helpers
{
    /// <summary>
    /// Helper class, just to gather all the properties in the xml under one tag
    /// </summary>
    class PropertiesDetails
    {
        public readonly List<string> Properties = new List<string>();

        public void AddProperty(string propName)
        {
            Properties.Add(propName);
        }
    }
}
