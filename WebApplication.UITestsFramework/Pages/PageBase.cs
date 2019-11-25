#region Using Directives

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

#endregion

namespace WebApplication.UITestFramework.Pages
{
    public abstract class PageBase
    {
        protected abstract string ControllerName { get; }
        protected abstract string PageTitle { get; }

        protected IWebElement DetailsFormHeader => By.Id($"{this.ControllerName}_DetailsFormHeader").FindElement(Browser.Driver);
        protected IWebElement SearchFormHeader => By.Id($"{this.ControllerName}_DetailsFormHeader").FindElement(Browser.Driver);

        protected IWebElement RefreshGridButton => By.Id($"{this.ControllerName}_RefreshGrid").FindElement(Browser.Driver);
        protected IWebElement NewEntityButton => By.Id($"{this.ControllerName}_NewEntityButton").FindElement(Browser.Driver);
        protected IWebElement DeleteButton => By.Id($"{this.ControllerName}_DeleteButton").FindElement(Browser.Driver);
        protected IWebElement SearchButton => By.Id($"{this.ControllerName}_OpenSearchCriteria").FindElement(Browser.Driver);


        protected IWebElement SaveModalButton => By.Id($"{this.ControllerName}_SaveButton").FindElement(Browser.Driver);
        protected IWebElement CloseModalButton => By.Id($"{this.ControllerName}_CloseModal").FindElement(Browser.Driver);
        protected IWebElement SaveAndCloseModalButton => By.Id($"{this.ControllerName}_SaveAndCloseModal").FindElement(Browser.Driver);

        public bool IsAt => Browser.WaitForTitle(this.PageTitle);

        public bool IsAtDetailsForm => this.DetailsFormHeader.Exists();

        public bool IsAtSearchForm => this.SearchFormHeader.Exists();

        public int TableRowsCount => By.CssSelector("table > tbody > tr").FindElements(Browser.Driver).Count;


        [FindsBy(How = How.ClassName, Using = "alert-success")]
        private IWebElement AlertSuccess { get; set; }


        public abstract void Goto();

        public bool? AlertSuccessExists()
        {
            bool exists = this.AlertSuccess.Exists();
            Browser.Wait.Until(By.ClassName("alert-success").DoesNotExist());
            return exists;
        }

        public void GoToAddNewForm()
        {
            this.NewEntityButton.Click();
            Browser.ChromeDriver.WaitForAjax();
        }

        public void GoToSearchForm()
        {
            this.SearchButton.Click();
            Browser.ChromeDriver.WaitForAjax();
        }

        public void SaveAndClose()
        {
            this.SaveAndCloseModalButton.Click();
            Browser.ChromeDriver.WaitForAjax();
        }

        public void Delete()
        {
            this.DeleteButton.Click();
            Thread.Sleep(1000);
            IWebElement confirmButton = By.XPath("//*[contains(text(), 'Confirm')]").FindElement(Browser.Driver);
            confirmButton.Click();
            Browser.ChromeDriver.WaitForAjax();
        }

        public void CloseModal()
        {
            this.CloseModalButton.Click();
            Browser.ChromeDriver.WaitForAjax();
        }

        public void FillForm(Dictionary<string, string> formValues, Dictionary<string, Dictionary<string, string>> formDropdownData = null)
        {
            formDropdownData = formDropdownData ?? new Dictionary<string, Dictionary<string, string>>();
            foreach (KeyValuePair<string, string> keyValuePair in formValues)
            {
                IWebElement field = By.Id(keyValuePair.Key).FindElement(Browser.Driver);
                field.SendKeys(keyValuePair.Value);
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in formDropdownData)
            {
                IWebElement field = By.Id(keyValuePair.Key).FindElement(Browser.Driver);
                var selectElement = new SelectElement(field);

                if (selectElement.Options.Any(x => x.Text == keyValuePair.Value["Name"]))
                {
                    selectElement.SelectByText(keyValuePair.Value["Name"]);
                }
                else
                {
                    Assert.Fail(keyValuePair.Value["Name"] + " Not found in the "+keyValuePair.Key+ " Select list");
                }
            }
        }
    }
}