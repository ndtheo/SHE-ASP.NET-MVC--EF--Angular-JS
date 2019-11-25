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
	public class MenuItemsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToMenuItemsPage()
		{
			Pages.MenuItems.Goto();
			Assert.IsTrue(Pages.MenuItems.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewMenuItem()
		{
			this.GoToMenuItemsPage();
			Pages.MenuItems.GoToAddNewForm();
			Assert.IsTrue(Pages.MenuItems.IsAtDetailsForm);
			Pages.MenuItems.FillForm(this.FormData);
			Pages.MenuItems.SaveAndClose();
			Assert.IsTrue(Pages.MenuItems.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueMenuItemNameCheck()
		{
			this.GoToMenuItemsPage();
			Pages.MenuItems.GoToAddNewForm();
			Assert.IsTrue(Pages.MenuItems.IsAtDetailsForm);
			Pages.MenuItems.FillForm(this.FormData);
			Assert.IsTrue(Pages.MenuItems.UniqueNameMessageVisible());
			Pages.MenuItems.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToMenuItemsPage();
			Pages.MenuItems.GoToSearchForm();
			Assert.IsTrue(Pages.MenuItems.IsAtSearchForm);
			Pages.MenuItems.FillForm(this.FormData);
			Pages.MenuItems.SaveAndClose();
			Assert.AreEqual(1, Pages.MenuItems.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.MenuItems.SelectFirstTableEntity();
			Pages.MenuItems.Delete();
			Assert.IsTrue(Pages.MenuItems.AlertSuccessExists());
			Assert.AreEqual(0, Pages.MenuItems.TableRowsCount);
		}
	}
}