#region Using Directives

using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

#endregion

namespace WebApplication
{
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        ///     The applications starting point
        /// </summary>
        protected void Application_Start()
        {

            const string LICENSE = "MTQwMzI3QDMxMzcyZTMyMmUzMFFBeVljUG94cjlRYzNNWWxnRzhyVVByaHVpdWdOM3VPNkNWY0d1WHJpYjg9;MTQwMzI4QDMxMzcyZTMyMmUzMG9aVGFyd0RpaWhzNDNNangwcWxKZHc3NU1XT0hJYlIxM05VZFkrM09CcWs9";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(LICENSE);
            // The RegisterAllAreas method finds all types in the application domain that derive from AreaRegistration and calls each of their RegisterArea methods.
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Registers all global filters for the MVC
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Registers all global filters for the WebApi
            FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}