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
	public class AccidentsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToAccidentsPage()
		{
			Pages.Accidents.Goto();
			Assert.IsTrue(Pages.Accidents.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewAccident()
		{
			this.GoToAccidentsPage();
			Pages.Accidents.GoToAddNewForm();
			Assert.IsTrue(Pages.Accidents.IsAtDetailsForm);
			Pages.Accidents.FillForm(this.FormData);
			Pages.Accidents.SaveAndClose();
			Assert.IsTrue(Pages.Accidents.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueAccidentNameCheck()
		{
			this.GoToAccidentsPage();
			Pages.Accidents.GoToAddNewForm();
			Assert.IsTrue(Pages.Accidents.IsAtDetailsForm);
			Pages.Accidents.FillForm(this.FormData);
			Assert.IsTrue(Pages.Accidents.UniqueNameMessageVisible());
			Pages.Accidents.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToAccidentsPage();
			Pages.Accidents.GoToSearchForm();
			Assert.IsTrue(Pages.Accidents.IsAtSearchForm);
			Pages.Accidents.FillForm(this.FormData);
			Pages.Accidents.SaveAndClose();
			Assert.AreEqual(1, Pages.Accidents.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Accidents.SelectFirstTableEntity();
			Pages.Accidents.Delete();
			Assert.IsTrue(Pages.Accidents.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Accidents.TableRowsCount);
		}
	}
}