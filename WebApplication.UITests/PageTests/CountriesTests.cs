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
	public class CountriesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToCountriesPage()
		{
			Pages.Countries.Goto();
			Assert.IsTrue(Pages.Countries.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewCountry()
		{
			this.GoToCountriesPage();
			Pages.Countries.GoToAddNewForm();
			Assert.IsTrue(Pages.Countries.IsAtDetailsForm);
			Pages.Countries.FillForm(this.FormData);
			Pages.Countries.SaveAndClose();
			Assert.IsTrue(Pages.Countries.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueCountryNameCheck()
		{
			this.GoToCountriesPage();
			Pages.Countries.GoToAddNewForm();
			Assert.IsTrue(Pages.Countries.IsAtDetailsForm);
			Pages.Countries.FillForm(this.FormData);
			Assert.IsTrue(Pages.Countries.UniqueNameMessageVisible());
			Pages.Countries.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToCountriesPage();
			Pages.Countries.GoToSearchForm();
			Assert.IsTrue(Pages.Countries.IsAtSearchForm);
			Pages.Countries.FillForm(this.FormData);
			Pages.Countries.SaveAndClose();
			Assert.AreEqual(1, Pages.Countries.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Countries.SelectFirstTableEntity();
			Pages.Countries.Delete();
			Assert.IsTrue(Pages.Countries.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Countries.TableRowsCount);
		}
	}
}