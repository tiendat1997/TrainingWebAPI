using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TrainingWebAPI.WebAPI.Logging;
using Unity;

namespace TrainingWebAPI.WebAPI.Filters
{
    public class LogActionWebApiFilter : ActionFilterAttribute
    {
        [Dependency]
        public LogHandler LogHandler { get; set; }
        //This function will execute before the web api controller
        //Part 2
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // pre-processing
            LogHandler.LogMessage(TracingLevel.INFO,TracingLayer.FILTER, "OnActionExecuted Request " + actionContext.Request.RequestUri.ToString());
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent != null)
            {
                var type = objectContent.ObjectType; //type of the returned object
                var value = objectContent.Value; //holding the returned value
            }

            LogHandler.LogMessage(TracingLevel.INFO, TracingLayer.FILTER, "OnActionExecuted Response " + actionExecutedContext.Response.StatusCode.ToString());
        }
    }
}