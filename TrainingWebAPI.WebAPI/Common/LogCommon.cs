using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingWebAPI.WebAPI.Common
{
    public static class LogMessageStatus
    {
        public static readonly string INVOKE = "INVOKE";
        public static readonly string EXECUTE_SUCCESSFULLY = "EXECUTE_SUCCESS";
        public static readonly string REQUEST_EXECUTING = "REQUEST_EXECUTING";
        public static readonly string RESPONSE_EXECUTED_SUCCESS = "RESPONSE_EXECUTED_SUCCESS";
        public static readonly string RESPONSE_EXECUTED_ERROR = "RESPONSE_EXECUTED_ERROR";
    }
    public static class DurationTracker
    {
        public static DateTime? LastTimeStamp;
    }
}