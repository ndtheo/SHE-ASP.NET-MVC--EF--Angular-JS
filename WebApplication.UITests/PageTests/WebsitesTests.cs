#region Using Directives

using System;
using System.Collections.Generic;
using NUnit.Framework;
using WebApplication.UITestFramework;
using WebApplication.UITestFramework.Pages;

#endregion

namespace WebApplication.UITests.PageTests
{
	[TestFixture]
	public class WebsitesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>
		{
		    ["Name"]= $"Test {Guid.NewGuid()}",
		    ["Url"] = "http://liquidmediademo.vision-solutions.gr/"
		};

		[Test, Order(1)]
		public void GoToWebsitesPage()
		{
			Pages.Websites.Goto();
			Assert.IsTrue(Pages.Websites.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewWebsite()
		{
			this.GoToWebsitesPage();
			Pages.Websites.GoToAddNewForm();
			Assert.IsTrue(Pages.Websites.IsAtDetailsForm);
			Pages.Websites.FillForm(this.FormData);
			Pages.Websites.SaveAndClose();
			Assert.IsTrue(Pages.Websites.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueWebsiteNameCheck()
		{
			this.GoToWebsitesPage();
			Pages.Websites.GoToAddNewForm();
			Assert.IsTrue(Pages.Websites.IsAtDetailsForm);
			Pages.Websites.FillForm(this.FormData);
			Assert.IsTrue(Pages.Websites.UniqueNameMessageVisible());
			Pages.Websites.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToWebsitesPage();
			Pages.Websites.GoToSearchForm();
			Assert.IsTrue(Pages.Websites.IsAtSearchForm);
			Pages.Websites.FillForm(this.FormData);
			Pages.Websites.SaveAndClose();
			Assert.AreEqual(1, Pages.Websites.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Websites.SelectFirstTableEntity();
			Pages.Websites.Delete();
			Assert.IsTrue(Pages.Websites.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Websites.TableRowsCount);
		}
	}
}