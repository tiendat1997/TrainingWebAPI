using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using TrainingWebAPI.WebAPI.Filters;
using TrainingWebAPI.WebAPI.Logging;

namespace TrainingWebAPI.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {           
            GlobalConfiguration.Configure(WebApiConfig.Register);     
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);          
        }
    }
}
