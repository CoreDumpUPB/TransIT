using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BestemAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes
    .Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
              name: "JobsApi2",
              routeTemplate: "api/Jobs/{type}/{status}/{vehicle}/{capacity}/{price}/{start_date}/{end_date}/{userid}/{start_loc}/{end_loc}",
              defaults: new { id = RouteParameter.Optional }
          );

            config.Routes.MapHttpRoute(
            name: "DefaultApi2",
            routeTemplate: "api/{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
        );

            config.Routes.MapHttpRoute(
          name: "SuggestJobsApi",
          routeTemplate: "api/{controller}/{UserID}/{lat1}/{long1}/{lat2}/{long2}/{transportType}"
      );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{email}/{password}/{phoneNumber}/{nume}",
                defaults: new { phoneNumber = RouteParameter.Optional, nume =  RouteParameter.Optional }
            );

        }
    }
}
