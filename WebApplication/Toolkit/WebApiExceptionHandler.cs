#region Using Directives

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

#endregion

namespace WebApplication.Toolkit
{
    [AttributeUsage(AttributeTargets.All)]
    internal class WebApiExceptionHandler : ActionFilterAttribute
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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

            log.Error(actionExecutedContext.Exception);

            var innerMessage = actionExecutedContext.Exception.InnerException?.Message;

            actionExecutedContext.Response = new HttpResponseMessage
            {
                Content = new StringContent(innerMessage),
                StatusCode = HttpStatusCode.InternalServerError,
            };
        }
    }
}