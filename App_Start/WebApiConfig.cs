using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApp2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "FilterApi",
            //    routeTemplate: "api/{controller}/{startYear}&{endYear}&{masContractor}&{masArticle}",
            //    defaults: new {  
            //        startYear = RouteParameter.Optional, 
            //        endYear = RouteParameter.Optional, 
            //        masContractor = RouteParameter.Optional, 
            //        masArticle = RouteParameter.Optional
            //    }
            //);
        }
    }
}
