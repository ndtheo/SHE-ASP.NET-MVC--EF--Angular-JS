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
	public class InsuranceCompaniesTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToInsuranceCompaniesPage()
		{
			Pages.InsuranceCompanies.Goto();
			Assert.IsTrue(Pages.InsuranceCompanies.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewInsuranceCompany()
		{
			this.GoToInsuranceCompaniesPage();
			Pages.InsuranceCompanies.GoToAddNewForm();
			Assert.IsTrue(Pages.InsuranceCompanies.IsAtDetailsForm);
			Pages.InsuranceCompanies.FillForm(this.FormData);
			Pages.InsuranceCompanies.SaveAndClose();
			Assert.IsTrue(Pages.InsuranceCompanies.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueInsuranceCompanyNameCheck()
		{
			this.GoToInsuranceCompaniesPage();
			Pages.InsuranceCompanies.GoToAddNewForm();
			Assert.IsTrue(Pages.InsuranceCompanies.IsAtDetailsForm);
			Pages.InsuranceCompanies.FillForm(this.FormData);
			Assert.IsTrue(Pages.InsuranceCompanies.UniqueNameMessageVisible());
			Pages.InsuranceCompanies.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToInsuranceCompaniesPage();
			Pages.InsuranceCompanies.GoToSearchForm();
			Assert.IsTrue(Pages.InsuranceCompanies.IsAtSearchForm);
			Pages.InsuranceCompanies.FillForm(this.FormData);
			Pages.InsuranceCompanies.SaveAndClose();
			Assert.AreEqual(1, Pages.InsuranceCompanies.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.InsuranceCompanies.SelectFirstTableEntity();
			Pages.InsuranceCompanies.Delete();
			Assert.IsTrue(Pages.InsuranceCompanies.AlertSuccessExists());
			Assert.AreEqual(0, Pages.InsuranceCompanies.TableRowsCount);
		}
	}
}