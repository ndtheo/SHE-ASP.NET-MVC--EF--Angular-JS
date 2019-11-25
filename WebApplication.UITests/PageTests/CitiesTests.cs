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
	public class CitiesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToCitiesPage()
		{
			Pages.Cities.Goto();
			Assert.IsTrue(Pages.Cities.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewCity()
		{
			this.GoToCitiesPage();
			Pages.Cities.GoToAddNewForm();
			Assert.IsTrue(Pages.Cities.IsAtDetailsForm);
			Pages.Cities.FillForm(this.FormData);
			Pages.Cities.SaveAndClose();
			Assert.IsTrue(Pages.Cities.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueCityNameCheck()
		{
			this.GoToCitiesPage();
			Pages.Cities.GoToAddNewForm();
			Assert.IsTrue(Pages.Cities.IsAtDetailsForm);
			Pages.Cities.FillForm(this.FormData);
			Assert.IsTrue(Pages.Cities.UniqueNameMessageVisible());
			Pages.Cities.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToCitiesPage();
			Pages.Cities.GoToSearchForm();
			Assert.IsTrue(Pages.Cities.IsAtSearchForm);
			Pages.Cities.FillForm(this.FormData);
			Pages.Cities.SaveAndClose();
			Assert.AreEqual(1, Pages.Cities.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Cities.SelectFirstTableEntity();
			Pages.Cities.Delete();
			Assert.IsTrue(Pages.Cities.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Cities.TableRowsCount);
		}
	}
}