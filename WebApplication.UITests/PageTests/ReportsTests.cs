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
	public class ReportsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToReportsPage()
		{
			Pages.Reports.Goto();
			Assert.IsTrue(Pages.Reports.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewReport()
		{
			this.GoToReportsPage();
			Pages.Reports.GoToAddNewForm();
			Assert.IsTrue(Pages.Reports.IsAtDetailsForm);
			Pages.Reports.FillForm(this.FormData);
			Pages.Reports.SaveAndClose();
			Assert.IsTrue(Pages.Reports.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueReportNameCheck()
		{
			this.GoToReportsPage();
			Pages.Reports.GoToAddNewForm();
			Assert.IsTrue(Pages.Reports.IsAtDetailsForm);
			Pages.Reports.FillForm(this.FormData);
			Assert.IsTrue(Pages.Reports.UniqueNameMessageVisible());
			Pages.Reports.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToReportsPage();
			Pages.Reports.GoToSearchForm();
			Assert.IsTrue(Pages.Reports.IsAtSearchForm);
			Pages.Reports.FillForm(this.FormData);
			Pages.Reports.SaveAndClose();
			Assert.AreEqual(1, Pages.Reports.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Reports.SelectFirstTableEntity();
			Pages.Reports.Delete();
			Assert.IsTrue(Pages.Reports.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Reports.TableRowsCount);
		}
	}
}