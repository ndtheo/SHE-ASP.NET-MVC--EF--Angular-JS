using System.Text.RegularExpressions;

namespace Utilities
{
    public static class StringHelper 
    {
        public static string AddUrlStrings(string sBeginning, string sEnd, bool finalFilePath = false)
        {
            string sV = string.Empty;
            if (!string.IsNullOrWhiteSpace(sBeginning) && !string.IsNullOrWhiteSpace(sEnd))
            {
                if (sBeginning.EndsWith("/") && sEnd.StartsWith("/"))
                {
                    sEnd = sEnd.Substring(1);
                }
                else if (!sBeginning.EndsWith("/") && !sEnd.StartsWith("/"))
                {
                    sBeginning += "/";
                }
                if (!finalFilePath && !sEnd.EndsWith("/"))
                {
                    sEnd += "/";
                }
                sV = sBeginning + sEnd;
            }
            return sV;
        }

        /// <summary>
        /// Adds two strings that constitute a path, securing that all directories end with \ 
        /// and there is no double \\
        /// 
        /// If the resulting path is a file instead of a directory, we set the finalFilePath parameter as true
        /// </summary>
        /// <param name="sBeginning"></param>
        /// <param name="sEnd"></param>
        /// <param name="finalFilePath"></param>
        /// <returns></returns>
        public static string AddPathStrings(string sBeginning, string sEnd, bool finalFilePath = false)
        {
            string sV = string.Empty;
            if (!string.IsNullOrWhiteSpace(sBeginning) && !string.IsNullOrWhiteSpace(sEnd))
            {
                if (sBeginning.EndsWith("\\") && sEnd.StartsWith("\\"))
                {
                    sEnd = sEnd.Substring(1);
                }
                else if (!sBeginning.EndsWith("\\") && !sEnd.StartsWith("\\"))
                {
                    sBeginning += "\\";
                }
                if (!finalFilePath && !sEnd.EndsWith("\\"))
                {
                    sEnd += "\\";
                }
                sV = sBeginning + sEnd;
            }
            return sV;
        }
        public static string RemoveHTMLTags(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("&nbsp;", " ");
                text = text.Replace("&amp;", "&");
                string noHTML = Regex.Replace(text, @"<[^>]+>", "").Trim();

                string noHTMLNormalised = Regex.Replace(noHTML, @"\s{2,}", " ");
                return noHTMLNormalised;
            }
            else
            {
                return "";
            }
        }
    }
}
