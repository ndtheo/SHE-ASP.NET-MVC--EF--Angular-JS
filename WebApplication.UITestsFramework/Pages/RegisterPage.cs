#region Using Directives

#region Using Directives

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebApplication.UITestFramework.Generators;
using WebApplication.UITestFramework.Models;

#endregion

#pragma warning disable 649

#endregion

namespace WebApplication.UITestFramework.Pages
{
    public class RegisterPage
    {
        [FindsBy(How = How.Id, Using = "email")] private IWebElement emailAddressTextField;

        [FindsBy(How = How.Id, Using = "password")] private IWebElement passwordTextField;

        [FindsBy(How = How.Id, Using = "confirmPassword")] private IWebElement confirmPasswordTextField;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit']")] private IWebElement registerButton;

        public void Goto()
        {
            Pages.TopNavigation.RegisterLink.Click();
        }

        public void RegisterNewUser()
        {
            User user = UserGenerator.Generate();

            this.emailAddressTextField.SendKeys(user.EmailAddress);
            this.passwordTextField.SendKeys(user.Password);
            this.confirmPasswordTextField.SendKeys(user.Password);

            this.registerButton.Click();
        }
    }
}