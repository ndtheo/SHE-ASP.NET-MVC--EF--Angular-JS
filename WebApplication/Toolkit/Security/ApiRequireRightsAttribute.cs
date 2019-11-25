#region Using Directives

using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using Core.Entities;
using Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApplication.VisionToolkit.ExtensionMethods;

#endregion

namespace WebApplication.VisionToolkit.Security
{
    // Usage: [RequireRights(Create=true, Delete=true)] ,  over an action.
    /// <summary>
    ///     Checks the user rights and allows access if the user thas the right required. See source for usage example
    /// </summary>
    /// <remarks>
    ///     System.Web.Http.Filters.FilterAttribute is different from the System.Web.Mvc.FilterAttribute  used in the Mvc.
    ///     Same for the IAuthenticationFilter Interface.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ApiRequireRightsAttribute : FilterAttribute, IAuthenticationFilter
    {
        public bool View { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Export { get; set; }

        /// <summary>
        ///     Authenticates the request.
        /// </summary>
        /// <returns>
        ///     A Task that will perform authentication.
        /// </returns>
        /// <param name="filterContext">The authentication context.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        public async Task AuthenticateAsync(HttpAuthenticationContext filterContext, CancellationToken cancellationToken)
        {
            var user = await GetUser();

            if (this.HasAuthorization(user, filterContext)) return;

            filterContext.ErrorResult = new StatusCodeResult(HttpStatusCode.Forbidden, filterContext.Request);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() => { }, cancellationToken); // just to stop the warning (async method should have an await operator)
        }

        private static async Task<User> GetUser()
        {
            return await HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindByIdAsync(CurrentUserId);
        }

        private bool HasAuthorization(User user, HttpAuthenticationContext filterContext)
        {
            var controllerName = GetCurrentControllerName(filterContext);
            return IdentityUserIsAuthenticated();
        }

        private static string GetCurrentControllerName(HttpAuthenticationContext filterContext)
        {
            return filterContext.Request.GetActionDescriptor().ControllerDescriptor.ControllerType.GetControllerName();
        }

        private static bool IdentityUserIsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        private static string CurrentUserId => HttpContext.Current.User.Identity.GetUserId();
    }
}