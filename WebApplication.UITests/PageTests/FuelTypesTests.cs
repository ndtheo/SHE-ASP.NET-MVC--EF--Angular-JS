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
	public class FuelTypesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToFuelTypesPage()
		{
			Pages.FuelTypes.Goto();
			Assert.IsTrue(Pages.FuelTypes.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewFuelType()
		{
			this.GoToFuelTypesPage();
			Pages.FuelTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.FuelTypes.IsAtDetailsForm);
			Pages.FuelTypes.FillForm(this.FormData);
			Pages.FuelTypes.SaveAndClose();
			Assert.IsTrue(Pages.FuelTypes.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueFuelTypeNameCheck()
		{
			this.GoToFuelTypesPage();
			Pages.FuelTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.FuelTypes.IsAtDetailsForm);
			Pages.FuelTypes.FillForm(this.FormData);
			Assert.IsTrue(Pages.FuelTypes.UniqueNameMessageVisible());
			Pages.FuelTypes.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToFuelTypesPage();
			Pages.FuelTypes.GoToSearchForm();
			Assert.IsTrue(Pages.FuelTypes.IsAtSearchForm);
			Pages.FuelTypes.FillForm(this.FormData);
			Pages.FuelTypes.SaveAndClose();
			Assert.AreEqual(1, Pages.FuelTypes.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.FuelTypes.SelectFirstTableEntity();
			Pages.FuelTypes.Delete();
			Assert.IsTrue(Pages.FuelTypes.AlertSuccessExists());
			Assert.AreEqual(0, Pages.FuelTypes.TableRowsCount);
		}
	}
}