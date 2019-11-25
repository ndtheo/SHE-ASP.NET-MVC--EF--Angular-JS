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
	public class DamagesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToDamagesPage()
		{
			Pages.Damages.Goto();
			Assert.IsTrue(Pages.Damages.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewDamage()
		{
			this.GoToDamagesPage();
			Pages.Damages.GoToAddNewForm();
			Assert.IsTrue(Pages.Damages.IsAtDetailsForm);
			Pages.Damages.FillForm(this.FormData);
			Pages.Damages.SaveAndClose();
			Assert.IsTrue(Pages.Damages.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueDamageNameCheck()
		{
			this.GoToDamagesPage();
			Pages.Damages.GoToAddNewForm();
			Assert.IsTrue(Pages.Damages.IsAtDetailsForm);
			Pages.Damages.FillForm(this.FormData);
			Assert.IsTrue(Pages.Damages.UniqueNameMessageVisible());
			Pages.Damages.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToDamagesPage();
			Pages.Damages.GoToSearchForm();
			Assert.IsTrue(Pages.Damages.IsAtSearchForm);
			Pages.Damages.FillForm(this.FormData);
			Pages.Damages.SaveAndClose();
			Assert.AreEqual(1, Pages.Damages.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Damages.SelectFirstTableEntity();
			Pages.Damages.Delete();
			Assert.IsTrue(Pages.Damages.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Damages.TableRowsCount);
		}
	}
}