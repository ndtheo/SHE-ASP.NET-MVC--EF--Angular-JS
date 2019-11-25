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
	public class ReportTypesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToReportTypesPage()
		{
			Pages.ReportTypes.Goto();
			Assert.IsTrue(Pages.ReportTypes.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewReportType()
		{
			this.GoToReportTypesPage();
			Pages.ReportTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.ReportTypes.IsAtDetailsForm);
			Pages.ReportTypes.FillForm(this.FormData);
			Pages.ReportTypes.SaveAndClose();
			Assert.IsTrue(Pages.ReportTypes.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueReportTypeNameCheck()
		{
			this.GoToReportTypesPage();
			Pages.ReportTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.ReportTypes.IsAtDetailsForm);
			Pages.ReportTypes.FillForm(this.FormData);
			Assert.IsTrue(Pages.ReportTypes.UniqueNameMessageVisible());
			Pages.ReportTypes.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToReportTypesPage();
			Pages.ReportTypes.GoToSearchForm();
			Assert.IsTrue(Pages.ReportTypes.IsAtSearchForm);
			Pages.ReportTypes.FillForm(this.FormData);
			Pages.ReportTypes.SaveAndClose();
			Assert.AreEqual(1, Pages.ReportTypes.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.ReportTypes.SelectFirstTableEntity();
			Pages.ReportTypes.Delete();
			Assert.IsTrue(Pages.ReportTypes.AlertSuccessExists());
			Assert.AreEqual(0, Pages.ReportTypes.TableRowsCount);
		}
	}
}