﻿#region Using Directives

using Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using WebApplication.Toolkit.ExtensionMethods;

#endregion

namespace WebApplication.Toolkit.Security
{
    // Usage: [RequireRights(Create=true, Delete=true)] ,  over an action
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     System.Web.Mvc.FilterAttribute is different from the System.Web.Http.Filters.FilterAttribute used in the
    ///     WebApi. Same for the IAuthenticationFilter Interface.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireRightsAttribute : FilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        ///     Authenticates the request.
        /// </summary>
        /// <param name="filterContext">The context to use for authentication.</param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = GetUser(filterContext);

            if (this.HasAuthorization(user, filterContext)) return;

            filterContext.Result = new HttpUnauthorizedResult();
        }

        /// <summary>
        ///     Adds an authentication challenge to the current <see cref="T:System.Web.Mvc.ActionResult" />.
        /// </summary>
        /// <param name="filterContext">The context to use for the authentication challenge.</param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) {}

        private static User GetUser(AuthenticationContext filterContext)
        {
            return filterContext.HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(CurrentUserId);
        }

        private bool HasAuthorization(User user, AuthenticationContext filterContext)
        {
            var controllerName = GetCurrentControllerName(filterContext);
            return IdentityUserIsAuthenticated(filterContext);
        }

        private static string GetCurrentControllerName(AuthenticationContext filterContext)
        {
            return filterContext.Controller.GetType().GetControllerName();
        }

        private static bool IdentityUserIsAuthenticated(AuthenticationContext filterContext)
        {
            return filterContext.HttpContext.User.Identity.IsAuthenticated;
        }

        private static string CurrentUserId => HttpContext.Current.User.Identity.GetUserId();
    }
}