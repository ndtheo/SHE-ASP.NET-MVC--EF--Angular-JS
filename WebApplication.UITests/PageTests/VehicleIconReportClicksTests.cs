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
	public class VehicleIconReportClicksTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToVehicleIconReportClicksPage()
		{
			Pages.VehicleIconReportClicks.Goto();
			Assert.IsTrue(Pages.VehicleIconReportClicks.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewVehicleIconReportClick()
		{
			this.GoToVehicleIconReportClicksPage();
			Pages.VehicleIconReportClicks.GoToAddNewForm();
			Assert.IsTrue(Pages.VehicleIconReportClicks.IsAtDetailsForm);
			Pages.VehicleIconReportClicks.FillForm(this.FormData);
			Pages.VehicleIconReportClicks.SaveAndClose();
			Assert.IsTrue(Pages.VehicleIconReportClicks.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueVehicleIconReportClickNameCheck()
		{
			this.GoToVehicleIconReportClicksPage();
			Pages.VehicleIconReportClicks.GoToAddNewForm();
			Assert.IsTrue(Pages.VehicleIconReportClicks.IsAtDetailsForm);
			Pages.VehicleIconReportClicks.FillForm(this.FormData);
			Assert.IsTrue(Pages.VehicleIconReportClicks.UniqueNameMessageVisible());
			Pages.VehicleIconReportClicks.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToVehicleIconReportClicksPage();
			Pages.VehicleIconReportClicks.GoToSearchForm();
			Assert.IsTrue(Pages.VehicleIconReportClicks.IsAtSearchForm);
			Pages.VehicleIconReportClicks.FillForm(this.FormData);
			Pages.VehicleIconReportClicks.SaveAndClose();
			Assert.AreEqual(1, Pages.VehicleIconReportClicks.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.VehicleIconReportClicks.SelectFirstTableEntity();
			Pages.VehicleIconReportClicks.Delete();
			Assert.IsTrue(Pages.VehicleIconReportClicks.AlertSuccessExists());
			Assert.AreEqual(0, Pages.VehicleIconReportClicks.TableRowsCount);
		}
	}
}