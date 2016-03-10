using EBooking.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EBooking
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new LogExceptionFilterAttribute());

            config.Routes.MapHttpRoute(
               name: "Api With Action",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           
        }
    }
}
