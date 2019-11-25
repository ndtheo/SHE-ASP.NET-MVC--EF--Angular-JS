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
	public class UserLogsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToUserLogsPage()
		{
			Pages.UserLogs.Goto();
			Assert.IsTrue(Pages.UserLogs.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewUserLog()
		{
			this.GoToUserLogsPage();
			Pages.UserLogs.GoToAddNewForm();
			Assert.IsTrue(Pages.UserLogs.IsAtDetailsForm);
			Pages.UserLogs.FillForm(this.FormData);
			Pages.UserLogs.SaveAndClose();
			Assert.IsTrue(Pages.UserLogs.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueUserLogNameCheck()
		{
			this.GoToUserLogsPage();
			Pages.UserLogs.GoToAddNewForm();
			Assert.IsTrue(Pages.UserLogs.IsAtDetailsForm);
			Pages.UserLogs.FillForm(this.FormData);
			Assert.IsTrue(Pages.UserLogs.UniqueNameMessageVisible());
			Pages.UserLogs.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToUserLogsPage();
			Pages.UserLogs.GoToSearchForm();
			Assert.IsTrue(Pages.UserLogs.IsAtSearchForm);
			Pages.UserLogs.FillForm(this.FormData);
			Pages.UserLogs.SaveAndClose();
			Assert.AreEqual(1, Pages.UserLogs.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.UserLogs.SelectFirstTableEntity();
			Pages.UserLogs.Delete();
			Assert.IsTrue(Pages.UserLogs.AlertSuccessExists());
			Assert.AreEqual(0, Pages.UserLogs.TableRowsCount);
		}
	}
}