using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity;

namespace TrainingWebAPI.WebAPI.Filters
{
    public class UnityActionFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IUnityContainer container;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">Unity container from UnityConfig.</param>
        public UnityActionFilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public new IEnumerable<FilterInfo> GetFilters(
                HttpConfiguration configuration,
                HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);

            foreach (var filter in filters)
            {
                container.BuildUp(filter.Instance.GetType(), filter.Instance);
            }

            return filters;
        }
    }
}