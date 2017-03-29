﻿using System;
using log4net.Core;
using System.Text;
using System.Collections.Generic;

namespace SimpleObjectAppender
{
    class SimpleForwarder : log4net.Appender.ForwardingAppender
    {
        const string defaultSeparator = ", ";
        const string defaultEqualsSymbol = ":";

        public string Separator { get; set; }
        public string EqualsSymbol { get; set; }
        public IDescriptor Details { get; set; }

        public override void ActivateOptions()
        {
            base.ActivateOptions();

            if (string.IsNullOrEmpty(EqualsSymbol))
            {
                EqualsSymbol = defaultEqualsSymbol;
            }

            if (string.IsNullOrEmpty(Separator))
            {
                Separator = defaultSeparator;
            }

            if (Details == null)
            {
                Details = new Descriptor();
            }
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
            var result = Details.getAllProperties(loggingEvent.MessageObject);
            var builder = new StringBuilder();
            int i = 0;
            foreach (var kvp in result)
            {
                builder.AppendFormat("{0}{1}{2}", kvp.Key, EqualsSymbol, kvp.Value);
                if (++i < result.Count)
                {
                    builder.Append(Separator);
                }
            }

            var data = loggingEvent.GetLoggingEventData(FixFlags.Message);
            data.Message = builder.ToString();
            return new LoggingEvent(data);
        }
    }
}
