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
	public class AccidentTypesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToAccidentTypesPage()
		{
			Pages.AccidentTypes.Goto();
			Assert.IsTrue(Pages.AccidentTypes.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewAccidentType()
		{
			this.GoToAccidentTypesPage();
			Pages.AccidentTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.AccidentTypes.IsAtDetailsForm);
			Pages.AccidentTypes.FillForm(this.FormData);
			Pages.AccidentTypes.SaveAndClose();
			Assert.IsTrue(Pages.AccidentTypes.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueAccidentTypeNameCheck()
		{
			this.GoToAccidentTypesPage();
			Pages.AccidentTypes.GoToAddNewForm();
			Assert.IsTrue(Pages.AccidentTypes.IsAtDetailsForm);
			Pages.AccidentTypes.FillForm(this.FormData);
			Assert.IsTrue(Pages.AccidentTypes.UniqueNameMessageVisible());
			Pages.AccidentTypes.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToAccidentTypesPage();
			Pages.AccidentTypes.GoToSearchForm();
			Assert.IsTrue(Pages.AccidentTypes.IsAtSearchForm);
			Pages.AccidentTypes.FillForm(this.FormData);
			Pages.AccidentTypes.SaveAndClose();
			Assert.AreEqual(1, Pages.AccidentTypes.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.AccidentTypes.SelectFirstTableEntity();
			Pages.AccidentTypes.Delete();
			Assert.IsTrue(Pages.AccidentTypes.AlertSuccessExists());
			Assert.AreEqual(0, Pages.AccidentTypes.TableRowsCount);
		}
	}
}