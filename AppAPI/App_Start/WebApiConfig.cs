using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using AppAPI.ActionFilters;

namespace AppAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Login",
                routeTemplate: "api/{controller}/{action}/{id}",
                constraints: new { controller = "Login" },
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "Signup",
                routeTemplate: "api/{controller}/{action}/{id}",
                constraints: new { controller = "Signup" },
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "Other routes",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                },
                constraints: null,
                handler: HttpClientFactory.CreatePipeline(
                    new HttpControllerDispatcher(config),
                    new DelegatingHandler[] { new UserValidationHandler() })
            );
        }
    }
}
