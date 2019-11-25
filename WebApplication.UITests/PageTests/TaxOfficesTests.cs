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
	public class TaxOfficesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToTaxOfficesPage()
		{
			Pages.TaxOffices.Goto();
			Assert.IsTrue(Pages.TaxOffices.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewTaxOffice()
		{
			this.GoToTaxOfficesPage();
			Pages.TaxOffices.GoToAddNewForm();
			Assert.IsTrue(Pages.TaxOffices.IsAtDetailsForm);
			Pages.TaxOffices.FillForm(this.FormData);
			Pages.TaxOffices.SaveAndClose();
			Assert.IsTrue(Pages.TaxOffices.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueTaxOfficeNameCheck()
		{
			this.GoToTaxOfficesPage();
			Pages.TaxOffices.GoToAddNewForm();
			Assert.IsTrue(Pages.TaxOffices.IsAtDetailsForm);
			Pages.TaxOffices.FillForm(this.FormData);
			Assert.IsTrue(Pages.TaxOffices.UniqueNameMessageVisible());
			Pages.TaxOffices.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToTaxOfficesPage();
			Pages.TaxOffices.GoToSearchForm();
			Assert.IsTrue(Pages.TaxOffices.IsAtSearchForm);
			Pages.TaxOffices.FillForm(this.FormData);
			Pages.TaxOffices.SaveAndClose();
			Assert.AreEqual(1, Pages.TaxOffices.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.TaxOffices.SelectFirstTableEntity();
			Pages.TaxOffices.Delete();
			Assert.IsTrue(Pages.TaxOffices.AlertSuccessExists());
			Assert.AreEqual(0, Pages.TaxOffices.TableRowsCount);
		}
	}
}