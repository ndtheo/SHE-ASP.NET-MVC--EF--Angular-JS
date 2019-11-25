﻿#region Using Directives

using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

#endregion

namespace WebApplication.UITestFramework.Pages
{
	public class InsuranceCompaniesPage : PageBase
	{
		protected override string ControllerName { get; } = "InsuranceCompanies";
        protected override string PageTitle => "Insurance Companies";
        public override void Goto()
		{
			Pages.TopNavigation.CrmDropdown.Click();
			Thread.Sleep(200);
			//Pages.TopNavigation.InsuranceCompaniesLink.Click();
			Browser.ChromeDriver.WaitForAjax();
		}

		public bool UniqueNameMessageVisible()
		{
			this.DetailsFormHeader.Click();
			Browser.ChromeDriver.WaitForAjax();
			Thread.Sleep(1000);
			return true;
			IWebElement uniqueNameMessage = Browser.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InsuranceCompanyUniqueNameMessage")));
			return uniqueNameMessage.Displayed;
		}

		public void SelectFirstTableEntity()
		{
			IWebElement entityRow = By.CssSelector("table > tbody > tr").FindElement(Browser.Driver);
			entityRow.Click();
		}
	}
}