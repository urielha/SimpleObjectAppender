using System;
using log4net.Core;
using System.Text;
using System.Collections.Generic;
using SimpleObjectAppender.Helpers;

namespace SimpleObjectAppender
{
    class SimpleObjectForwarder : log4net.Appender.ForwardingAppender
    {
        const string defaultSeparator = ", ";
        const string defaultEqualsSymbol = ":";

        public string Separator { get; set; }
        public string EqualsSymbol { get; set; }
        public bool IgnoreNotExists { get; set; }
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
            var data = loggingEvent.GetLoggingEventData(FixFlags.Message);
            data.Message = ObjectToString(loggingEvent.MessageObject);
            return new LoggingEvent(data);
        }

        public string ObjectToString(object obj)
        {
            int i = 0;
            var builder = new StringBuilder();
            foreach (var propertyName in Details.Properties)
            {
                var value = Descriptor.getPropertyValue(obj, propertyName);
                if (value != null || !IgnoreNotExists)
                {
                    builder.AppendFormat("{0}{1}{2}", propertyName, EqualsSymbol, value);
                    if (++i < Details.Properties.Count)
                    {
                        builder.Append(Separator);
                    }
                }
            }

            return builder.ToString();
        }
    }
}
