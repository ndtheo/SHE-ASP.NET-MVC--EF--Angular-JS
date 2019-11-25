#region Using Directives

using System;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

#endregion

namespace WebApplication.UITestFramework
{
    public static class Browser
    {
        private static string baseUrl = "http://liquidmediademo.vision-solutions.gr/";
        public static TimeSpan DefaultTimeSpan = TimeSpan.FromSeconds(5);

        private static IWebDriver _chromeDriver;
        public static WebDriverWait Wait;

        static Browser()
        {
            Browser._chromeDriver = new ChromeDriver();
            Browser.Wait = new WebDriverWait(Browser._chromeDriver, Browser.DefaultTimeSpan);

            Browser._chromeDriver.Manage().Timeouts().ImplicitWait = Browser.DefaultTimeSpan;
            Browser._chromeDriver.Manage().Window.Position = Point.Empty;
            Browser._chromeDriver.Manage().Window.Maximize();
            var options = new ChromeOptions();
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddArgument("test-type");
        }


        public static void Initialize()
        {
            
        }

        public static string Title => Browser._chromeDriver.Title;

        public static ISearchContext Driver => Browser._chromeDriver;
        public static IWebDriver ChromeDriver => Browser._chromeDriver;

        public static void Goto(string url)
        {
            Browser._chromeDriver.Url = Browser.baseUrl + url;
        }

        public static void Close()
        {
           
        }

        public static bool WaitForTitle(string title)
        {
            return Browser.Wait.Until(ExpectedConditions.TitleContains(title));
        }
    }
}