using System;
using log4net.Core;
using System.Text;
using System.Collections.Generic;

namespace SimpleObjectAppender
{
    class SimpleObjectForwarder : log4net.Appender.ForwardingAppender
    {
        const string defaultSeparator = ", ";
        const string defaultEqualsSymbol = ":";

        public string Separator { get; set; }
        public string EqualsSymbol { get; set; }
        public Boolean IgnoreNotExists { get; set; }
        public IDescriptor Descriptor { get; set; }
        public PropertiesDetails Details { get; set; }

        /// <summary>
        /// Constructor to set defaults values for all params
        /// </summary>
        public SimpleObjectForwarder()
        {
            IgnoreNotExists = true;
            EqualsSymbol = defaultEqualsSymbol;
            Separator = defaultSeparator;
            Descriptor = new Descriptor();
            Details = new PropertiesDetails();
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            base.Append(ProcessLogEvent(loggingEvent));
        }

        protected override void Append(LoggingEvent[] loggingEvents)
        {
            var processedEvents = new List<LoggingEvent>(loggingEvents.Length);
            foreach (var logEvent in loggingEvents)
            {
                processedEvents.Add(ProcessLogEvent(logEvent));
            }
            base.Append(processedEvents.ToArray());
        }

        private LoggingEvent ProcessLogEvent(LoggingEvent loggingEvent)
        {
            int i = 0;
            var builder = new StringBuilder();
            foreach (var propertyName in Details.Properties)
            {
                var value = Descriptor.getPropertyValue(loggingEvent.MessageObject, propertyName);
                if (value != null || !IgnoreNotExists)
                {
                    builder.AppendFormat("{0}{1}{2}", propertyName, EqualsSymbol, value);
                    if (++i < Details.Properties.Count)
                    {
                        builder.Append(Separator);
                    }
                }
            }

            var data = loggingEvent.GetLoggingEventData(FixFlags.Message);
            data.Message = builder.ToString();
            return new LoggingEvent(data);
        }
    }

    class PropertiesDetails
    {
        public readonly List<string> Properties = new List<string>();

        public void AddProperty(string propName)
        {
            Properties.Add(propName);
        }
    }
}
