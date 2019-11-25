#region Using Directives

using System;
using OpenQA.Selenium;
using WebApplication.UITestFramework.Enums;
using WebApplication.UITestFramework.Generators;
using WebApplication.UITestFramework.Models;

#endregion

namespace WebApplication.UITestFramework.Pages
{
    public class LoginPage
    {
        private static IWebElement EmailAddressTextField => By.Id("Email").FindElement(Browser.Driver);
        private static IWebElement PasswordTextField => By.Id("Password").FindElement(Browser.Driver);
        private static IWebElement LogInButton => By.CssSelector("input[type='submit']").FindElement(Browser.Driver);

        private const string LogInUrl = "Account/Login";
        private const string PredifinedUserName = "f.karagiorgos@vision-solutions.gr";
        private const string PredifinedPassword = "testertester";

        public void LogInAsLastRegisteredUser() => LoginPage.LogIn(UserGenerator.LastGeneratedUser);

        public void LogIn(LoginOptions loginOptions)
        {
            var user = new User();
            switch (loginOptions)
            {
                case LoginOptions.UseLastGeneratedPassword:
                    user.EmailAddress = UserGenerator.LastGeneratedUser.EmailAddress;
                    user.Password = PasswordGenerator.LastGeneratedPassword;
                    break;
                case LoginOptions.PredefinedCredentials:
                    user.EmailAddress = LoginPage.PredifinedUserName;
                    user.Password = LoginPage.PredifinedPassword;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(loginOptions), loginOptions, null);
            }

            LoginPage.LogIn(user);
        }

        private static void LogIn(User user)
        {
            LoginPage.EmailAddressTextField.SendKeys(user.EmailAddress);
            LoginPage.PasswordTextField.SendKeys(user.Password);

            LoginPage.LogInButton.Click();
        }

        public void Goto() => Browser.Goto(LoginPage.LogInUrl);
    }
}