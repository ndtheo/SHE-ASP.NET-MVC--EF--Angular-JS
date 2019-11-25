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
	public class DamageCategoriesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToDamageCategoriesPage()
		{
			Pages.DamageCategories.Goto();
			Assert.IsTrue(Pages.DamageCategories.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewDamageCategory()
		{
			this.GoToDamageCategoriesPage();
			Pages.DamageCategories.GoToAddNewForm();
			Assert.IsTrue(Pages.DamageCategories.IsAtDetailsForm);
			Pages.DamageCategories.FillForm(this.FormData);
			Pages.DamageCategories.SaveAndClose();
			Assert.IsTrue(Pages.DamageCategories.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueDamageCategoryNameCheck()
		{
			this.GoToDamageCategoriesPage();
			Pages.DamageCategories.GoToAddNewForm();
			Assert.IsTrue(Pages.DamageCategories.IsAtDetailsForm);
			Pages.DamageCategories.FillForm(this.FormData);
			Assert.IsTrue(Pages.DamageCategories.UniqueNameMessageVisible());
			Pages.DamageCategories.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToDamageCategoriesPage();
			Pages.DamageCategories.GoToSearchForm();
			Assert.IsTrue(Pages.DamageCategories.IsAtSearchForm);
			Pages.DamageCategories.FillForm(this.FormData);
			Pages.DamageCategories.SaveAndClose();
			Assert.AreEqual(1, Pages.DamageCategories.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.DamageCategories.SelectFirstTableEntity();
			Pages.DamageCategories.Delete();
			Assert.IsTrue(Pages.DamageCategories.AlertSuccessExists());
			Assert.AreEqual(0, Pages.DamageCategories.TableRowsCount);
		}
	}
}