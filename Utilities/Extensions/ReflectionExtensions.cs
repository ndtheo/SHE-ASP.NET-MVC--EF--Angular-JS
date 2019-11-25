#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

#endregion

namespace Utilities.Extensions
{
    /// <summary>Extension methods for the <see cref="System.Reflection" /> namespace.</summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        ///     Wrapper of the <see cref="Type.GetProperty(string)" /> method that allows getting the child property of a
        ///     property by string. ("AdUnit.Placement.Name")
        /// </summary>
        /// <param name="type">A Type variable</param>
        /// <param name="propertyName">The string of the property name</param>
        public static PropertyInfo GetSubProperty(this Type type, string propertyName)
        {
            if (type == null || String.IsNullOrWhiteSpace(propertyName))
                return null;

            var propNames = propertyName.Split('.');
            PropertyInfo propInfo = null;
            var propType = type;

            foreach (var propName in propNames)
            {
                propInfo = propType?.GetProperty(propName);
                propType = propInfo?.PropertyType;
            }
            return propInfo;
        }

        /// <summary>
        ///     Returns a <see cref="List{PropertyInfo}" /> contaning all the Property infos of a nested property of a
        ///     <see cref="System.Type" /> Object. ("AdUnit.Placement.Name").
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        public static List<PropertyInfo> GetPropertyHierarhy(this Type type, string propertyName)
        {
            if (type == null || String.IsNullOrWhiteSpace(propertyName))
                return null;

            var propNames = propertyName.Split('.');
            var propertiesList = new List<PropertyInfo>();
            var propName = propNames[0];

            propertiesList.Add(type.GetProperty(propName));
            for (var i = 1; i < propNames.Length; i++)
            {
                propName += "." + propNames[i];
                propertiesList.Add(type.GetSubProperty(propName));
            }
            return propertiesList;
        }

        /// <summary>Returns the value of the last property, in a property hierarhy list, for an object.</summary>
        /// <param name="propHierarhy"></param>
        /// <param name="data"></param>
        /// <typeparam name="T"></typeparam>
        public static object GetPropHierarhyValue<T>(this List<PropertyInfo> propHierarhy, T data)
        {
            if (propHierarhy == null || data == null)
                return null;

            var value = propHierarhy[0]?.GetValue(data);

            for (var i = 1; i < propHierarhy.Count; i++)
            {
                if (value != null)
                    value = propHierarhy[i]?.GetValue(value);
            }
            return value;
        }

        /// <summary>Returns the <see cref="System.Type" /> of a Generic collection.</summary>
        /// <param name="_"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Type Type<T>(this ICollection<T> _) => typeof(T);

        /// <summary>Returns the display name of a property based on the <see cref="DisplayAttribute.Name" /> property</summary>
        /// <param name="property"></param>
        public static string GetDisplayName(this PropertyInfo property)
        {
            if (property == null)
                return String.Empty;

            var displayAttribute = (DisplayAttribute) Attribute.GetCustomAttribute(property, typeof(DisplayAttribute));
            return displayAttribute?.Name ?? property.Name;
        }
    }
}