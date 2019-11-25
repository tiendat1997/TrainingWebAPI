using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TrainingWebAPI.WebAPI.Common;
using TrainingWebAPI.WebAPI.Logging;
using Unity;

namespace TrainingWebAPI.WebAPI.Filters
{
    public class LogActionWebApiFilter : ActionFilterAttribute
    {
        [Dependency]
        public LogHandler LogHandler { get; set; }
        private readonly string layer = TracingLayer.FILTER.ToString();
        private readonly string controllerLayer = TracingLayer.CONTROLLER.ToString();
        //This function will execute before the web api controller
        //Part 2
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // pre-processing
            DurationTracker.LastTimeStamp = null;
            LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}:{1}] {2}", layer, LogMessageStatus.REQUEST_EXECUTING, actionContext.Request.RequestUri.ToString()));
            string controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string action = actionContext.ActionDescriptor.ActionName;
            LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}:{1}] {2} -> {3}", controllerLayer, LogMessageStatus.INVOKE, controller, action));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}:{1}] {2}", layer, LogMessageStatus.RESPONSE_EXECUTED_SUCCESS, actionExecutedContext.Response.StatusCode.ToString()));
            }
            else
            {
                LogHandler.LogException(TracingLevel.ERROR, actionExecutedContext.Exception);
                LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}:{1}] {2}", layer, LogMessageStatus.RESPONSE_EXECUTED_ERROR, "Response end up with error"));
            }
        }
    }
}