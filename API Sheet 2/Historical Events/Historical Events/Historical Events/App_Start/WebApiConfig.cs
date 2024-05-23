using Historical_Events.Basic_Authorisation;
using Historical_Events.Helpers;
using System.Web.Http;

namespace Historical_Events
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Global Filters
            config.Filters.Add(new BasicAuthenticationFilter());
            config.Filters.Add(new ValidateModelAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
