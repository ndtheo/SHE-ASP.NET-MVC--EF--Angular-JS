#region Using Directives

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Utilities.Extensions;

#endregion

namespace WebApplication.Toolkit.ExtensionMethods
{
    /// <summary>Extension methods for the <see cref="string" /> class</summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Returns the template given having replaced all the keywords inside the {{keyword}} brancets with the maching
        ///     property values
        /// </summary>
        public static string ReplaceTagsInTemplate<T>(this string template, T model)
        {
            if (template == null)
                return string.Empty;

            var matches = Regex.Matches(template, @"\{{([^}]*)\}}");

            var rowTags = (from Match match in matches select match?.Value).ToList();
            var tags = rowTags.Select(rowTag => rowTag?.Replace("{{", string.Empty).Replace("}}", string.Empty)).ToList();

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");

            for (var i = 0; i < tags.Count; i++)
            {
                var tag = tags[i];
                var obj = typeof(T).GetPropertyHierarhy(tag)?.GetPropHierarhyValue(model);
                var value = (obj as DateTime?)?.ToShortDateString() ??
                               (obj as decimal?)?.ToString("N") ??
                               (obj as int?)?.ToString("N0") ??
                               obj?.ToString() ??
                               string.Empty;
                if (rowTags[i] != null)
                    template = template.Replace(rowTags[i], value);
            }
            return template;
        }
    }
}