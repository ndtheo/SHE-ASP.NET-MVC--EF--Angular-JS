#region Using Directives

using System.Web.Http.Filters;
using System.Web.Mvc;

#endregion

namespace WebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }

        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            // Logs Exceptions and Returns web messages
            filters.Add(new System.Web.Http.AuthorizeAttribute());
        }
    }
}