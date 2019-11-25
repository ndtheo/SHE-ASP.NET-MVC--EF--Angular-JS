#region Using Directives

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

#endregion

namespace WebApplication.VisionToolkit.ExtensionMethods
{
    internal static class ReflectionExtensions
    {
        /// <summary>Returns a <see cref="MemberInfo.Name" /> property value, removing the "Controller" suffix.</summary>
        /// <param name="info">The member info of a controller class.</param>
        public static string GetControllerName(this MemberInfo info) => info?.Name.Replace("Controller", string.Empty);
    }
}