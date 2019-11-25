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
	public class ActivitiesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToActivitiesPage()
		{
			Pages.Activities.Goto();
			Assert.IsTrue(Pages.Activities.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewActivity()
		{
			this.GoToActivitiesPage();
			Pages.Activities.GoToAddNewForm();
			Assert.IsTrue(Pages.Activities.IsAtDetailsForm);
			Pages.Activities.FillForm(this.FormData);
			Pages.Activities.SaveAndClose();
			Assert.IsTrue(Pages.Activities.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueActivityNameCheck()
		{
			this.GoToActivitiesPage();
			Pages.Activities.GoToAddNewForm();
			Assert.IsTrue(Pages.Activities.IsAtDetailsForm);
			Pages.Activities.FillForm(this.FormData);
			Assert.IsTrue(Pages.Activities.UniqueNameMessageVisible());
			Pages.Activities.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToActivitiesPage();
			Pages.Activities.GoToSearchForm();
			Assert.IsTrue(Pages.Activities.IsAtSearchForm);
			Pages.Activities.FillForm(this.FormData);
			Pages.Activities.SaveAndClose();
			Assert.AreEqual(1, Pages.Activities.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Activities.SelectFirstTableEntity();
			Pages.Activities.Delete();
			Assert.IsTrue(Pages.Activities.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Activities.TableRowsCount);
		}
	}
}