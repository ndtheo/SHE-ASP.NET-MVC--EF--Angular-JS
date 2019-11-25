#region Using Directives

using System;
using System.Threading;
using OpenQA.Selenium;

#endregion

namespace WebApplication.UITestFramework
{
    public static class Extensions
    {
        public static bool Exists(this IWebElement element)
        {
            try
            {
                string text = element.Text;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static Func<IWebDriver, bool> DoesNotExist(this By locator)
        {
            return driver =>
            {
                Browser.ChromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
                try
                {
                    return !driver.FindElement(locator).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
                finally
                {
                    Browser.ChromeDriver.Manage().Timeouts().ImplicitWait = Browser.DefaultTimeSpan;
                }
            };
        }

        public static void WaitForAjax(this IWebDriver driver, int timeoutSecs = 10, bool throwException = true)
        {
            for (var i = 0; i < timeoutSecs; i++)
            {
                bool jQueryComplete = ((IJavaScriptExecutor) driver).ExecuteScript("return jQuery.active").ToString() == "0";
                bool javascriptComplete = ((IJavaScriptExecutor) driver).ExecuteScript("return document.readyState").ToString() == "complete";
                bool angularComplete = (bool)((IJavaScriptExecutor) driver).ExecuteScript("return (window.angular !== undefined) && (angular.element(document.body).injector() !== undefined) && (angular.element(document.body).injector().get('$http').pendingRequests.length === 0)");
                if (jQueryComplete && javascriptComplete && angularComplete) return;
                Thread.Sleep(1000);
            }
            if (throwException)
                throw new Exception("WebDriver timed out waiting for AJAX call to complete");
        }
    }
}