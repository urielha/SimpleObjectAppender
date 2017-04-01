using System;
using log4net.Core;
using System.Text;
using System.Collections.Generic;
using SimpleObjectAppender.Helpers;

namespace SimpleObjectAppender
{
    class SimpleObjectConsoleAppender : log4net.Appender.AppenderSkeleton
    {
        private SimpleObjectForwarder objectForwarder;
        public string Separator
        {
            get { return objectForwarder.Separator; }
            set { objectForwarder.Separator = value; }
        } 
        public string EqualsSymbol
        {
            get { return objectForwarder.EqualsSymbol; }
            set { objectForwarder.EqualsSymbol = value; }
        }
        public bool IgnoreNotExists {
            get { return objectForwarder.IgnoreNotExists; }
            set { objectForwarder.IgnoreNotExists = value; }
        }
        public IDescriptor Descriptor {
            get { return objectForwarder.Descriptor; }
            set { objectForwarder.Descriptor = value; }
        }
        public PropertiesDetails Details {
            get { return objectForwarder.Details; }
            set { objectForwarder.Details = value; }
        }

        public SimpleObjectConsoleAppender()
        {
            objectForwarder = new SimpleObjectForwarder();
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            Console.WriteLine(objectForwarder.ObjectToString(loggingEvent.MessageObject));
        }
    }
}
