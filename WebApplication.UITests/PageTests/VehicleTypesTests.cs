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
	public class VehicleTypesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToVehicleTypesPage()
		{
			Pages.VehicleTypes.Goto();
			Assert.IsTrue(Pages.VehicleTypes.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewVehicleType()
		{
			this.GoToVehicleTypesPage();
			Pages.VehicleTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.VehicleTypes.IsAtDetailsForm);
			Pages.VehicleTypes.FillForm(this.FormData);
			Pages.VehicleTypes.SaveAndClose();
			Assert.IsTrue(Pages.VehicleTypes.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueVehicleTypeNameCheck()
		{
			this.GoToVehicleTypesPage();
			Pages.VehicleTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.VehicleTypes.IsAtDetailsForm);
			Pages.VehicleTypes.FillForm(this.FormData);
			Assert.IsTrue(Pages.VehicleTypes.UniqueNameMessageVisible());
			Pages.VehicleTypes.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToVehicleTypesPage();
			Pages.VehicleTypes.GoToSearchForm();
			Assert.IsTrue(Pages.VehicleTypes.IsAtSearchForm);
			Pages.VehicleTypes.FillForm(this.FormData);
			Pages.VehicleTypes.SaveAndClose();
			Assert.AreEqual(1, Pages.VehicleTypes.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.VehicleTypes.SelectFirstTableEntity();
			Pages.VehicleTypes.Delete();
			Assert.IsTrue(Pages.VehicleTypes.AlertSuccessExists());
			Assert.AreEqual(0, Pages.VehicleTypes.TableRowsCount);
		}
	}
}