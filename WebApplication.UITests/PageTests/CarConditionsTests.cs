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
	public class CarConditionsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToCarConditionsPage()
		{
			Pages.CarConditions.Goto();
			Assert.IsTrue(Pages.CarConditions.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewCarCondition()
		{
			this.GoToCarConditionsPage();
			Pages.CarConditions.GoToAddNewForm();
			Assert.IsTrue(Pages.CarConditions.IsAtDetailsForm);
			Pages.CarConditions.FillForm(this.FormData);
			Pages.CarConditions.SaveAndClose();
			Assert.IsTrue(Pages.CarConditions.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueCarConditionNameCheck()
		{
			this.GoToCarConditionsPage();
			Pages.CarConditions.GoToAddNewForm();
			Assert.IsTrue(Pages.CarConditions.IsAtDetailsForm);
			Pages.CarConditions.FillForm(this.FormData);
			Assert.IsTrue(Pages.CarConditions.UniqueNameMessageVisible());
			Pages.CarConditions.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToCarConditionsPage();
			Pages.CarConditions.GoToSearchForm();
			Assert.IsTrue(Pages.CarConditions.IsAtSearchForm);
			Pages.CarConditions.FillForm(this.FormData);
			Pages.CarConditions.SaveAndClose();
			Assert.AreEqual(1, Pages.CarConditions.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.CarConditions.SelectFirstTableEntity();
			Pages.CarConditions.Delete();
			Assert.IsTrue(Pages.CarConditions.AlertSuccessExists());
			Assert.AreEqual(0, Pages.CarConditions.TableRowsCount);
		}
	}
}