using System.Web.Http;
using WebActivatorEx;
using WebApp2;
using Swashbuckle.Application;

//[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApp2
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
            .EnableSwagger(c => c.SingleApiVersion("v2", "API"))
            .EnableSwaggerUi();
        }
    }
}
