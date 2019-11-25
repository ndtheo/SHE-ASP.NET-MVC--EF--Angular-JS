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
	public class RepairShopsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToRepairShopsPage()
		{
			Pages.RepairShops.Goto();
			Assert.IsTrue(Pages.RepairShops.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewRepairShop()
		{
			this.GoToRepairShopsPage();
			Pages.RepairShops.GoToAddNewForm();
			Assert.IsTrue(Pages.RepairShops.IsAtDetailsForm);
			Pages.RepairShops.FillForm(this.FormData);
			Pages.RepairShops.SaveAndClose();
			Assert.IsTrue(Pages.RepairShops.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueRepairShopNameCheck()
		{
			this.GoToRepairShopsPage();
			Pages.RepairShops.GoToAddNewForm();
			Assert.IsTrue(Pages.RepairShops.IsAtDetailsForm);
			Pages.RepairShops.FillForm(this.FormData);
			Assert.IsTrue(Pages.RepairShops.UniqueNameMessageVisible());
			Pages.RepairShops.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToRepairShopsPage();
			Pages.RepairShops.GoToSearchForm();
			Assert.IsTrue(Pages.RepairShops.IsAtSearchForm);
			Pages.RepairShops.FillForm(this.FormData);
			Pages.RepairShops.SaveAndClose();
			Assert.AreEqual(1, Pages.RepairShops.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.RepairShops.SelectFirstTableEntity();
			Pages.RepairShops.Delete();
			Assert.IsTrue(Pages.RepairShops.AlertSuccessExists());
			Assert.AreEqual(0, Pages.RepairShops.TableRowsCount);
		}
	}
}