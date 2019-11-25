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
	public class CarModelsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToCarModelsPage()
		{
			Pages.CarModels.Goto();
			Assert.IsTrue(Pages.CarModels.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewCarModel()
		{
			this.GoToCarModelsPage();
			Pages.CarModels.GoToAddNewForm();
			Assert.IsTrue(Pages.CarModels.IsAtDetailsForm);
			Pages.CarModels.FillForm(this.FormData);
			Pages.CarModels.SaveAndClose();
			Assert.IsTrue(Pages.CarModels.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueCarModelNameCheck()
		{
			this.GoToCarModelsPage();
			Pages.CarModels.GoToAddNewForm();
			Assert.IsTrue(Pages.CarModels.IsAtDetailsForm);
			Pages.CarModels.FillForm(this.FormData);
			Assert.IsTrue(Pages.CarModels.UniqueNameMessageVisible());
			Pages.CarModels.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToCarModelsPage();
			Pages.CarModels.GoToSearchForm();
			Assert.IsTrue(Pages.CarModels.IsAtSearchForm);
			Pages.CarModels.FillForm(this.FormData);
			Pages.CarModels.SaveAndClose();
			Assert.AreEqual(1, Pages.CarModels.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.CarModels.SelectFirstTableEntity();
			Pages.CarModels.Delete();
			Assert.IsTrue(Pages.CarModels.AlertSuccessExists());
			Assert.AreEqual(0, Pages.CarModels.TableRowsCount);
		}
	}
}