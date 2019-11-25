#region Using Directives

using OpenQA.Selenium;
using WebApplication.UITestFramework.Generators;

#endregion

namespace WebApplication.UITestFramework.Pages
{
    public class ManageAccountPage
    {
        private static IWebElement CurrentPasswordTextField => By.Id("currentPassword").FindElement(Browser.Driver);
        private static IWebElement NewPasswordTextField => By.Id("newPassword").FindElement(Browser.Driver);
        private static IWebElement ConfirmPasswordTextField => By.Id("confirmPassword").FindElement(Browser.Driver);
        private static IWebElement ChangePasswordButton => By.CssSelector("button[type='submit']").FindElement(Browser.Driver);


        public void Goto()
        {
            Pages.TopNavigation.ManageLink.Click();
        }

        public void ChangePassword()
        {
            ManageAccountPage.CurrentPasswordTextField.SendKeys(UserGenerator.LastGeneratedUser.Password);
            ManageAccountPage.NewPasswordTextField.SendKeys(PasswordGenerator.Generate());
            ManageAccountPage.ConfirmPasswordTextField.SendKeys(PasswordGenerator.LastGeneratedPassword);
            ManageAccountPage.ChangePasswordButton.Click();
        }
    }
}