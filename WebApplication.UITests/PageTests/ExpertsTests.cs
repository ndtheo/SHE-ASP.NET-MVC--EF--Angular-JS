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
	public class ExpertsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToExpertsPage()
		{
			Pages.Experts.Goto();
			Assert.IsTrue(Pages.Experts.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewExpert()
		{
			this.GoToExpertsPage();
			Pages.Experts.GoToAddNewForm();
			Assert.IsTrue(Pages.Experts.IsAtDetailsForm);
			Pages.Experts.FillForm(this.FormData);
			Pages.Experts.SaveAndClose();
			Assert.IsTrue(Pages.Experts.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueExpertNameCheck()
		{
			this.GoToExpertsPage();
			Pages.Experts.GoToAddNewForm();
			Assert.IsTrue(Pages.Experts.IsAtDetailsForm);
			Pages.Experts.FillForm(this.FormData);
			Assert.IsTrue(Pages.Experts.UniqueNameMessageVisible());
			Pages.Experts.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToExpertsPage();
			Pages.Experts.GoToSearchForm();
			Assert.IsTrue(Pages.Experts.IsAtSearchForm);
			Pages.Experts.FillForm(this.FormData);
			Pages.Experts.SaveAndClose();
			Assert.AreEqual(1, Pages.Experts.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Experts.SelectFirstTableEntity();
			Pages.Experts.Delete();
			Assert.IsTrue(Pages.Experts.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Experts.TableRowsCount);
		}
	}
}