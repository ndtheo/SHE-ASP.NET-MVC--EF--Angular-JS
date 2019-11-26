#region Using Directives

using Core.Entities;
using Database.Models.DbContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using WebApplication.Toolkit.ExtensionMethods;

#endregion

namespace WebApplication.Toolkit.BaseControllers
{
    /// <summary>
    ///     Base Controller that all our system's controllers should inherit from, in order to have database
    ///     and some standard properties.
    /// </summary>
    public class BaseController : Controller
      {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            /// <summary>An <see cref="IncidentsDataContext" /> object for our controllers to access the database.</summary>
            protected readonly IncidentsDataContext db = new IncidentsDataContext();

        /// <summary>Returns the Application User object of the currently logged in user.</summary>
        protected User ApplicationUser => this.ApplicationUserManager.FindById(this.UserId);

        /// <summary>Returns the Application User manager of the owin context.</summary>
        protected ApplicationUserManager ApplicationUserManager => this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        /// <summary>The Id of the currently logged in user.</summary>
        protected string UserId => this.User?.Identity.GetUserId();

        /// <summary>Called before the action method is invoked.</summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                base.OnActionExecuting(filterContext);
            ///Do we do a query for every action?
            this.ViewBag.ControllerName = filterContext.Controller.GetType().GetControllerName();
        }

        /// <summary>
        ///     Called when an unhandled exception occurs in the action. We override it in order to log the systems
        ///     exceptions.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            log.Error(filterContext?.Exception);
            base.OnException(filterContext);
        }

        /// <summary>Releases unmanaged resources and optionally releases managed resources.</summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
          this.db.Dispose();
          base.Dispose(disposing);
        }

        /// <summary>Creates a CustomJsonResult for the data given.</summary>
        /// <param name="data"></param>
        protected CustomJsonResult CustomJson(object data)
        {
            return new CustomJsonResult { Data = data};
        }
    }
}
