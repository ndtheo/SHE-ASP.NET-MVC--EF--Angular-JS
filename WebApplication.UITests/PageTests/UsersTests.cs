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
	public class UsersTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToUsersPage()
		{
			Pages.Users.Goto();
			Assert.IsTrue(Pages.Users.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewUser()
		{
			this.GoToUsersPage();
			Pages.Users.GoToAddNewForm();
			Assert.IsTrue(Pages.Users.IsAtDetailsForm);
			Pages.Users.FillForm(this.FormData);
			Pages.Users.SaveAndClose();
			Assert.IsTrue(Pages.Users.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueUserNameCheck()
		{
			this.GoToUsersPage();
			Pages.Users.GoToAddNewForm();
			Assert.IsTrue(Pages.Users.IsAtDetailsForm);
			Pages.Users.FillForm(this.FormData);
			Assert.IsTrue(Pages.Users.UniqueNameMessageVisible());
			Pages.Users.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToUsersPage();
			Pages.Users.GoToSearchForm();
			Assert.IsTrue(Pages.Users.IsAtSearchForm);
			Pages.Users.FillForm(this.FormData);
			Pages.Users.SaveAndClose();
			Assert.AreEqual(1, Pages.Users.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Users.SelectFirstTableEntity();
			Pages.Users.Delete();
			Assert.IsTrue(Pages.Users.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Users.TableRowsCount);
		}
	}
}