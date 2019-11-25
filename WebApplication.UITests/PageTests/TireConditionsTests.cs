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
	public class TireConditionsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToTireConditionsPage()
		{
			Pages.TireConditions.Goto();
			Assert.IsTrue(Pages.TireConditions.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewTireCondition()
		{
			this.GoToTireConditionsPage();
			Pages.TireConditions.GoToAddNewForm();
			Assert.IsTrue(Pages.TireConditions.IsAtDetailsForm);
			Pages.TireConditions.FillForm(this.FormData);
			Pages.TireConditions.SaveAndClose();
			Assert.IsTrue(Pages.TireConditions.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueTireConditionNameCheck()
		{
			this.GoToTireConditionsPage();
			Pages.TireConditions.GoToAddNewForm();
			Assert.IsTrue(Pages.TireConditions.IsAtDetailsForm);
			Pages.TireConditions.FillForm(this.FormData);
			Assert.IsTrue(Pages.TireConditions.UniqueNameMessageVisible());
			Pages.TireConditions.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToTireConditionsPage();
			Pages.TireConditions.GoToSearchForm();
			Assert.IsTrue(Pages.TireConditions.IsAtSearchForm);
			Pages.TireConditions.FillForm(this.FormData);
			Pages.TireConditions.SaveAndClose();
			Assert.AreEqual(1, Pages.TireConditions.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.TireConditions.SelectFirstTableEntity();
			Pages.TireConditions.Delete();
			Assert.IsTrue(Pages.TireConditions.AlertSuccessExists());
			Assert.AreEqual(0, Pages.TireConditions.TableRowsCount);
		}
	}
}