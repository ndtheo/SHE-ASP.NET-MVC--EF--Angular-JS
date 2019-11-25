#region Using Directives

using System;

#endregion

namespace Utilities
{
    public static class TryFunc
    {
        /// <summary>
        ///     Tries to execute a func, ignoring any exception
        /// </summary>
        public static void IgnoreException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        ///     Tries to execute a func, and returns the result. In case of an exception, it returns the default value of <see cref="T" />
        /// </summary>
        public static T IgnoreException<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}