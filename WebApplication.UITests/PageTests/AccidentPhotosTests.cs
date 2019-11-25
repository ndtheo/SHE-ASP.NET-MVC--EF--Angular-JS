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
	public class AccidentPhotoesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToAccidentPhotoesPage()
		{
			Pages.AccidentPhotoes.Goto();
			Assert.IsTrue(Pages.AccidentPhotoes.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewAccidentPhoto()
		{
			this.GoToAccidentPhotoesPage();
			Pages.AccidentPhotoes.GoToAddNewForm();
			Assert.IsTrue(Pages.AccidentPhotoes.IsAtDetailsForm);
			Pages.AccidentPhotoes.FillForm(this.FormData);
			Pages.AccidentPhotoes.SaveAndClose();
			Assert.IsTrue(Pages.AccidentPhotoes.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueAccidentPhotoNameCheck()
		{
			this.GoToAccidentPhotoesPage();
			Pages.AccidentPhotoes.GoToAddNewForm();
			Assert.IsTrue(Pages.AccidentPhotoes.IsAtDetailsForm);
			Pages.AccidentPhotoes.FillForm(this.FormData);
			Assert.IsTrue(Pages.AccidentPhotoes.UniqueNameMessageVisible());
			Pages.AccidentPhotoes.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToAccidentPhotoesPage();
			Pages.AccidentPhotoes.GoToSearchForm();
			Assert.IsTrue(Pages.AccidentPhotoes.IsAtSearchForm);
			Pages.AccidentPhotoes.FillForm(this.FormData);
			Pages.AccidentPhotoes.SaveAndClose();
			Assert.AreEqual(1, Pages.AccidentPhotoes.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.AccidentPhotoes.SelectFirstTableEntity();
			Pages.AccidentPhotoes.Delete();
			Assert.IsTrue(Pages.AccidentPhotoes.AlertSuccessExists());
			Assert.AreEqual(0, Pages.AccidentPhotoes.TableRowsCount);
		}
	}
}