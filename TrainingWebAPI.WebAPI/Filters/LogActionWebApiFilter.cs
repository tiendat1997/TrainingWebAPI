using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TrainingWebAPI.WebAPI.Logging;
using Unity;

namespace TrainingWebAPI.WebAPI.Filters
{
    public class LogActionWebApiFilter : ActionFilterAttribute, IExceptionFilter
    {
        [Dependency]
        public LogHandler LogHandler { get; set; }
        //This function will execute before the web api controller
        //Part 2
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // pre-processing
            LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}] OnActionExecuting Request: {1}", TracingLayer.FILTER.ToString(), actionContext.Request.RequestUri.ToString()));
            string controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string action = actionContext.ActionDescriptor.ActionName;
            LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}] {1},{2}:Invoke", TracingLayer.CONTROLLER.ToString(), controller, action));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}] OnActionExecuted Response: {1}", TracingLayer.FILTER.ToString(), actionExecutedContext.Response.StatusCode.ToString()));
            }
        }
        
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            Action action = () =>
            {
                LogHandler.LogMessage(TracingLevel.ERROR, "", actionExecutedContext.Exception);
            };
            var task = new Task(action);
            task.Start();
            return task;
        }
    }
}