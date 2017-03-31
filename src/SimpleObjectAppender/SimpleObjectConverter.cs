using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using log4net.Util;

namespace SimpleObjectAppender
{
    class SimpleObjectConverter : log4net.Layout.Pattern.PatternLayoutConverter
    {
        public IDescriptor Descriptor { get; set; }

        public SimpleObjectConverter()
        {
            Descriptor = new Descriptor();
        }
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (!string.IsNullOrEmpty(Option))
            {
                var value = Descriptor.getPropertyValue(loggingEvent.MessageObject, Option);
                if (value != null) {
                    WriteObject(writer, loggingEvent.Repository, value);
                }
            }
        }
    }
}
