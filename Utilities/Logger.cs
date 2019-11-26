#region Using Directives

using System;
using System.IO;
using System.Web;

#endregion

namespace Utilities
{
    /// <summary>
    /// We could utilize log4net, which has multiple capabilities.
    /// </summary>
    public static class Log
    {
        private static string Path { get; }

        static Log()
        {
            Path = GetDefaultLogPath();
            CreateLogDirectory();
        }

        public static void Exception(Exception ex)
        {
            TryFunc.IgnoreException(() =>
            {
                File.AppendAllText($"{Path}ExceptionDetailsLog.txt", $"\n\n\n{DateTime.Now:s}: {ex?.ToString() ?? "Exception object is null"}");
                File.AppendAllText($"{Path}ExceptionLog.txt", $"\n\n{DateTime.Now:s}: {ex.GetInnerExceptionMessage() ?? "Exception object is null"}");
            });
        }

        public static string GetInnerExceptionMessage(this Exception exception)
        {
            while (exception?.InnerException != null)
            {
                exception = exception.InnerException;
            }

            return exception?.Message;
        }

        private static void CreateLogDirectory()
        {
            if (!Directory.Exists(Path))
                TryFunc.IgnoreException(() => Directory.CreateDirectory(Path));
        }

        private static string GetDefaultLogPath()
        {
            return (TryFunc.IgnoreException(() => HttpRuntime.AppDomainAppPath + "App_Data\\") ?? string.Empty) + "Logs\\";
        }
    }
}