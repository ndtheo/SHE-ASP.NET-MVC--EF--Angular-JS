#region Using Directives

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Utilities;

#endregion

namespace WebApplication.Toolkit
{
    [AttributeUsage(AttributeTargets.All)]
    internal class WebApiExceptionHandler : ActionFilterAttribute
    {

        /// <summary>Occurs before the action method is invoked.</summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
        }

        /// <summary>Occurs after the action method is invoked.</summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
                return;

            Log.Exception(actionExecutedContext.Exception);

            var innerMessage = actionExecutedContext.Exception.GetInnerExceptionMessage();

            actionExecutedContext.Response = new HttpResponseMessage
            {
                Content = new StringContent(innerMessage),
                StatusCode = HttpStatusCode.InternalServerError,
            };
        }
    }
}