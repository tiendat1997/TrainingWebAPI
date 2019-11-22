using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace TrainingWebAPI.WebAPI.Filters
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            RegisterFilterProviders(); //<-- register your logging filter.
        }

        private static void RegisterFilterProviders()
        {
            var providers =
                GlobalConfiguration.Configuration.Services.GetFilterProviders().ToList();

            GlobalConfiguration.Configuration.Services.Add(
                typeof(System.Web.Http.Filters.IFilterProvider),
                new UnityActionFilterProvider(UnityConfig.Container));

            var defaultprovider = providers.First(p => p is ActionDescriptorFilterProvider);

            GlobalConfiguration.Configuration.Services.Remove(
                typeof(System.Web.Http.Filters.IFilterProvider),
                defaultprovider);
        }
    }
}