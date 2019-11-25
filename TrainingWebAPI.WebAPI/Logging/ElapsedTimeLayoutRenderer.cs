using NLog;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TrainingWebAPI.WebAPI.Common;

namespace TrainingWebAPI.WebAPI.Logging
{
    
    [LayoutRenderer("elapsed-time")]
    public class ElapsedTimeLayoutRenderer : LayoutRenderer
    {       
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            var lastTimeStamp = DurationTracker.LastTimeStamp ?? logEvent.TimeStamp;
            var elapsedTime = logEvent.TimeStamp - lastTimeStamp;
            builder.Append(elapsedTime.TotalMilliseconds);
            DurationTracker.LastTimeStamp = logEvent.TimeStamp;
        }
    }
}