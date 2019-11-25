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
	public class RolesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToRolesPage()
		{
			Pages.Roles.Goto();
			Assert.IsTrue(Pages.Roles.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewRole()
		{
			this.GoToRolesPage();
			Pages.Roles.GoToAddNewForm();
			Assert.IsTrue(Pages.Roles.IsAtDetailsForm);
			Pages.Roles.FillForm(this.FormData);
			Pages.Roles.SaveAndClose();
			Assert.IsTrue(Pages.Roles.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueRoleNameCheck()
		{
			this.GoToRolesPage();
			Pages.Roles.GoToAddNewForm();
			Assert.IsTrue(Pages.Roles.IsAtDetailsForm);
			Pages.Roles.FillForm(this.FormData);
			Assert.IsTrue(Pages.Roles.UniqueNameMessageVisible());
			Pages.Roles.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToRolesPage();
			Pages.Roles.GoToSearchForm();
			Assert.IsTrue(Pages.Roles.IsAtSearchForm);
			Pages.Roles.FillForm(this.FormData);
			Pages.Roles.SaveAndClose();
			Assert.AreEqual(1, Pages.Roles.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Roles.SelectFirstTableEntity();
			Pages.Roles.Delete();
			Assert.IsTrue(Pages.Roles.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Roles.TableRowsCount);
		}
	}
}