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
	public class CarBrandsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToCarBrandsPage()
		{
			Pages.CarBrands.Goto();
			Assert.IsTrue(Pages.CarBrands.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewCarBrand()
		{
			this.GoToCarBrandsPage();
			Pages.CarBrands.GoToAddNewForm();
			Assert.IsTrue(Pages.CarBrands.IsAtDetailsForm);
			Pages.CarBrands.FillForm(this.FormData);
			Pages.CarBrands.SaveAndClose();
			Assert.IsTrue(Pages.CarBrands.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueCarBrandNameCheck()
		{
			this.GoToCarBrandsPage();
			Pages.CarBrands.GoToAddNewForm();
			Assert.IsTrue(Pages.CarBrands.IsAtDetailsForm);
			Pages.CarBrands.FillForm(this.FormData);
			Assert.IsTrue(Pages.CarBrands.UniqueNameMessageVisible());
			Pages.CarBrands.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToCarBrandsPage();
			Pages.CarBrands.GoToSearchForm();
			Assert.IsTrue(Pages.CarBrands.IsAtSearchForm);
			Pages.CarBrands.FillForm(this.FormData);
			Pages.CarBrands.SaveAndClose();
			Assert.AreEqual(1, Pages.CarBrands.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.CarBrands.SelectFirstTableEntity();
			Pages.CarBrands.Delete();
			Assert.IsTrue(Pages.CarBrands.AlertSuccessExists());
			Assert.AreEqual(0, Pages.CarBrands.TableRowsCount);
		}
	}
}