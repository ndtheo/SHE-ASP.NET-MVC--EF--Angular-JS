#region Using Directives

using System.Net.Http;
using System.Web.Http;
using Core.Entities;
using Database.Models.DbContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication.VisionToolkit;

#endregion

namespace WebApplication.BaseControllers
{
    /// <summary>
    ///     Base Api Controller that all our system's controllers should inherit from, in order to have database, security
    ///     and some standard properties.
    /// </summary>
    [WebApiExceptionHandler]
    public class BaseApiController : ApiController
    {
        /// <summary>An <see cref="IncidentsDataContext" /> object for our controllers to access the database.</summary>
        protected readonly IncidentsDataContext db = new IncidentsDataContext();

        /// <summary>Returns the ApplicationUser object of the currently logged in user.</summary>
        protected User ApplicationUser => this.Request.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(this.UserId);

        /// <summary>Returns the Id of the currently logged in user.</summary>
        protected string UserId => this.User?.Identity.GetUserId();

        /// <summary>Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.</summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
    }
}