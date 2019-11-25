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
	public class SizesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToSizesPage()
		{
			Pages.Sizes.Goto();
			Assert.IsTrue(Pages.Sizes.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewSize()
		{
			this.GoToSizesPage();
			Pages.Sizes.GoToAddNewForm();
			Assert.IsTrue(Pages.Sizes.IsAtDetailsForm);
			Pages.Sizes.FillForm(this.FormData);
			Pages.Sizes.SaveAndClose();
			Assert.IsTrue(Pages.Sizes.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueSizeNameCheck()
		{
			this.GoToSizesPage();
			Pages.Sizes.GoToAddNewForm();
			Assert.IsTrue(Pages.Sizes.IsAtDetailsForm);
			Pages.Sizes.FillForm(this.FormData);
			Assert.IsTrue(Pages.Sizes.UniqueNameMessageVisible());
			Pages.Sizes.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToSizesPage();
			Pages.Sizes.GoToSearchForm();
			Assert.IsTrue(Pages.Sizes.IsAtSearchForm);
			Pages.Sizes.FillForm(this.FormData);
			Pages.Sizes.SaveAndClose();
			Assert.AreEqual(1, Pages.Sizes.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Sizes.SelectFirstTableEntity();
			Pages.Sizes.Delete();
			Assert.IsTrue(Pages.Sizes.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Sizes.TableRowsCount);
		}
	}
}