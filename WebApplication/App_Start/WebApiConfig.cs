#region Using Directives

using System.Net.Http.Formatting;
using System.Web.Http;
using Utilities;
using Utilities.CustomJsonContractResolver;
using WebApplication.VisionToolkit;

#endregion

namespace WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Return only json
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new GetOnlyContractResolver();
            // Web API routes
            config.MapHttpAttributeRoutes();

            // default routing
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new
            {
                id = RouteParameter.Optional
            });
        }
    }
}