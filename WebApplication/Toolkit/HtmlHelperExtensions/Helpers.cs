#region Using Directives

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

#endregion

namespace WebApplication.Toolkit.HtmlHelperExtensions
{
    public static class Helpers
    { 

        public static string ControllerName(this HtmlHelper html)
        {
            return (string) html?.ViewBag?.ControllerName ?? "";
        }

        /// <summary>
        ///     True if the htmlhelper has to check rights for edit. EditMode is assigned in most of the details actions.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static bool IsEditMode(this HtmlHelper html)
        {
            return html?.ViewBag?.EditMode ?? false;
        }

        /// <summary>
        ///     True if the a view is loaded in tab mode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static bool IsTab(this HtmlHelper html)
        {
            return html?.ViewBag?.IsTab ?? false;
        }

        public static bool IsRequired<T, TV>(this Expression<Func<T, TV>> expression)
        {
            if (!(expression?.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<RequiredAttribute>() != null;
        }

        public static bool IsReadOnly<T, TV>(this Expression<Func<T, TV>> expression)
        {
            if (!(expression?.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<ReadOnlyAttribute>()?.IsReadOnly ?? false;
        }

        public static DataTypeAttribute GetDataTypeAttribute<T, TV>(this Expression<Func<T, TV>> expression)
        {
            if (!(expression?.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<DataTypeAttribute>();
        }

        private static DefaultValueAttribute GetDefaultValueAttribute<T, TV>(this Expression<Func<T, TV>> expression)
        {
            if (!(expression?.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<DefaultValueAttribute>();
        }

        public static RangeAttribute GetRangeAttribute<T, TV>(this Expression<Func<T, TV>> expression)
        {
            if (!(expression?.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<RangeAttribute>();
        }
        public static StringLengthAttribute GetStringLengthAttribute<T, TV>(this Expression<Func<T, TV>> expression)
        {
            if (!(expression?.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be a member expression");

            return memberExpression.Member.GetAttribute<StringLengthAttribute>();
        }

        private static T GetAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute
        {
            var type = typeof(T);
            var attributes = provider.GetCustomAttributes(type, true);
            return attributes.Length > 0 ? attributes[0] as T : null;
        }
    }
}