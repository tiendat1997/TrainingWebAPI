using System.Web.Http;
using WebActivatorEx;
using TrainingWebAPI.WebAPI;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace TrainingWebAPI.WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {                       
                        c.SingleApiVersion("v1", "TrainingWebAPI.WebAPI");                      
                    })
                .EnableSwaggerUi(c =>
                    {                       
                    });
        }
    }
}
