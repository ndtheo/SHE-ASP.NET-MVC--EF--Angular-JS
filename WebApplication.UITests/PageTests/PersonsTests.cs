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
	public class PeopleTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToPeoplePage()
		{
			Pages.People.Goto();
			Assert.IsTrue(Pages.People.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewPerson()
		{
			this.GoToPeoplePage();
			Pages.People.GoToAddNewForm();
			Assert.IsTrue(Pages.People.IsAtDetailsForm);
			Pages.People.FillForm(this.FormData);
			Pages.People.SaveAndClose();
			Assert.IsTrue(Pages.People.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniquePersonNameCheck()
		{
			this.GoToPeoplePage();
			Pages.People.GoToAddNewForm();
			Assert.IsTrue(Pages.People.IsAtDetailsForm);
			Pages.People.FillForm(this.FormData);
			Assert.IsTrue(Pages.People.UniqueNameMessageVisible());
			Pages.People.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToPeoplePage();
			Pages.People.GoToSearchForm();
			Assert.IsTrue(Pages.People.IsAtSearchForm);
			Pages.People.FillForm(this.FormData);
			Pages.People.SaveAndClose();
			Assert.AreEqual(1, Pages.People.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.People.SelectFirstTableEntity();
			Pages.People.Delete();
			Assert.IsTrue(Pages.People.AlertSuccessExists());
			Assert.AreEqual(0, Pages.People.TableRowsCount);
		}
	}
}