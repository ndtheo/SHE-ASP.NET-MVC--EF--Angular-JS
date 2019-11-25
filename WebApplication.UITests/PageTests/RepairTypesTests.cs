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
	public class RepairTypesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToRepairTypesPage()
		{
			Pages.RepairTypes.Goto();
			Assert.IsTrue(Pages.RepairTypes.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewRepairType()
		{
			this.GoToRepairTypesPage();
			Pages.RepairTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.RepairTypes.IsAtDetailsForm);
			Pages.RepairTypes.FillForm(this.FormData);
			Pages.RepairTypes.SaveAndClose();
			Assert.IsTrue(Pages.RepairTypes.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueRepairTypeNameCheck()
		{
			this.GoToRepairTypesPage();
			Pages.RepairTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.RepairTypes.IsAtDetailsForm);
			Pages.RepairTypes.FillForm(this.FormData);
			Assert.IsTrue(Pages.RepairTypes.UniqueNameMessageVisible());
			Pages.RepairTypes.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToRepairTypesPage();
			Pages.RepairTypes.GoToSearchForm();
			Assert.IsTrue(Pages.RepairTypes.IsAtSearchForm);
			Pages.RepairTypes.FillForm(this.FormData);
			Pages.RepairTypes.SaveAndClose();
			Assert.AreEqual(1, Pages.RepairTypes.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.RepairTypes.SelectFirstTableEntity();
			Pages.RepairTypes.Delete();
			Assert.IsTrue(Pages.RepairTypes.AlertSuccessExists());
			Assert.AreEqual(0, Pages.RepairTypes.TableRowsCount);
		}
	}
}