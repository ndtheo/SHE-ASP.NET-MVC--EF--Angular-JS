#region Using Directives

using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

#endregion

namespace WebApplication.VisionToolkit
{
    /// <summary>Custom json serializer for use with the mvc controller for using the same dateformat as the WebApi.</summary>
    public class CustomJsonResult : JsonResult
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        ///     Enables processing of the result of an action method by a custom type that inherits from the
        ///     <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="context" /> parameter is null.</exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context?.HttpContext?.Response == null)
                throw new ArgumentNullException(nameof(context));

            var response = context.HttpContext?.Response;

            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";
            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null) return;
            // Using Json.NET serializer
            var isoConvert = new IsoDateTimeConverter
            {
                DateTimeFormat = CustomJsonResult.DateFormat
            };
            response.Write(JsonConvert.SerializeObject(this.Data, isoConvert));
        }
    }
}