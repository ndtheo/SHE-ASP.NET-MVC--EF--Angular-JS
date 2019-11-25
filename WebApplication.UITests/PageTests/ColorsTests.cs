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
	public class ColorsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToColorsPage()
		{
			Pages.Colors.Goto();
			Assert.IsTrue(Pages.Colors.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewColor()
		{
			this.GoToColorsPage();
			Pages.Colors.GoToAddNewForm();
			Assert.IsTrue(Pages.Colors.IsAtDetailsForm);
			Pages.Colors.FillForm(this.FormData);
			Pages.Colors.SaveAndClose();
			Assert.IsTrue(Pages.Colors.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueColorNameCheck()
		{
			this.GoToColorsPage();
			Pages.Colors.GoToAddNewForm();
			Assert.IsTrue(Pages.Colors.IsAtDetailsForm);
			Pages.Colors.FillForm(this.FormData);
			Assert.IsTrue(Pages.Colors.UniqueNameMessageVisible());
			Pages.Colors.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToColorsPage();
			Pages.Colors.GoToSearchForm();
			Assert.IsTrue(Pages.Colors.IsAtSearchForm);
			Pages.Colors.FillForm(this.FormData);
			Pages.Colors.SaveAndClose();
			Assert.AreEqual(1, Pages.Colors.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Colors.SelectFirstTableEntity();
			Pages.Colors.Delete();
			Assert.IsTrue(Pages.Colors.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Colors.TableRowsCount);
		}
	}
}