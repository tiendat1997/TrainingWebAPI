using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TrainingWebAPI.WebAPI.Logging
{
    public enum TracingLayer
    {
        FILTER, CONTROLLER, BUSINESS_LOGIC, DATA_ACCESS
    }
    public enum TracingLevel
    {
        ALL, DEBUG, INFO, WARN, ERROR, FATAL, OFF
    }

    public class LogHandler
    {
        private static Logger Log => LogManager.GetCurrentClassLogger();      
       
        public void LogMessage(TracingLevel level, string message)
        {
            switch (level)
            {
                case TracingLevel.DEBUG:
                    Log.Debug(message);
                    break;

                case TracingLevel.INFO:
                    Log.Info(message);
                    break;

                case TracingLevel.WARN:
                    Log.Warn(message);
                    break;
            }
        }
        public void LogException(TracingLevel level, Exception exception)
        {
            switch (level)
            {              
                case TracingLevel.ERROR:
                    Log.Error(exception);
                    break;

                case TracingLevel.FATAL:
                    Log.Fatal(exception);
                    break;
            }
        }

    }
}