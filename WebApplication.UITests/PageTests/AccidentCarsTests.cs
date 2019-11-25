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
	public class AccidentCarsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToAccidentCarsPage()
		{
			Pages.AccidentCars.Goto();
			Assert.IsTrue(Pages.AccidentCars.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewAccidentCar()
		{
			this.GoToAccidentCarsPage();
			Pages.AccidentCars.GoToAddNewForm();
			Assert.IsTrue(Pages.AccidentCars.IsAtDetailsForm);
			Pages.AccidentCars.FillForm(this.FormData);
			Pages.AccidentCars.SaveAndClose();
			Assert.IsTrue(Pages.AccidentCars.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueAccidentCarNameCheck()
		{
			this.GoToAccidentCarsPage();
			Pages.AccidentCars.GoToAddNewForm();
			Assert.IsTrue(Pages.AccidentCars.IsAtDetailsForm);
			Pages.AccidentCars.FillForm(this.FormData);
			Assert.IsTrue(Pages.AccidentCars.UniqueNameMessageVisible());
			Pages.AccidentCars.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToAccidentCarsPage();
			Pages.AccidentCars.GoToSearchForm();
			Assert.IsTrue(Pages.AccidentCars.IsAtSearchForm);
			Pages.AccidentCars.FillForm(this.FormData);
			Pages.AccidentCars.SaveAndClose();
			Assert.AreEqual(1, Pages.AccidentCars.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.AccidentCars.SelectFirstTableEntity();
			Pages.AccidentCars.Delete();
			Assert.IsTrue(Pages.AccidentCars.AlertSuccessExists());
			Assert.AreEqual(0, Pages.AccidentCars.TableRowsCount);
		}
	}
}