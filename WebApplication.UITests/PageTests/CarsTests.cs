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
	public class CarsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToCarsPage()
		{
			Pages.Cars.Goto();
			Assert.IsTrue(Pages.Cars.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewCar()
		{
			this.GoToCarsPage();
			Pages.Cars.GoToAddNewForm();
			Assert.IsTrue(Pages.Cars.IsAtDetailsForm);
			Pages.Cars.FillForm(this.FormData);
			Pages.Cars.SaveAndClose();
			Assert.IsTrue(Pages.Cars.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueCarNameCheck()
		{
			this.GoToCarsPage();
			Pages.Cars.GoToAddNewForm();
			Assert.IsTrue(Pages.Cars.IsAtDetailsForm);
			Pages.Cars.FillForm(this.FormData);
			Assert.IsTrue(Pages.Cars.UniqueNameMessageVisible());
			Pages.Cars.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToCarsPage();
			Pages.Cars.GoToSearchForm();
			Assert.IsTrue(Pages.Cars.IsAtSearchForm);
			Pages.Cars.FillForm(this.FormData);
			Pages.Cars.SaveAndClose();
			Assert.AreEqual(1, Pages.Cars.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Cars.SelectFirstTableEntity();
			Pages.Cars.Delete();
			Assert.IsTrue(Pages.Cars.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Cars.TableRowsCount);
		}
	}
}