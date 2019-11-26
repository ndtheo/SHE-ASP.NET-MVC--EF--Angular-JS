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
	public class IncidentsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{["Name"]= $"Test {Guid.NewGuid()}"};

		[Test, Order(1)]
		public void GoToIncidentPage()
		{
			Pages.Incidents.Goto();
			Assert.IsTrue(Pages.Incidents.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewIncident()
		{
			this.GoToIncidentsPage();
			Pages.Incidents.GoToAddNewForm();
			Assert.IsTrue(Pages.Incidents.IsAtDetailsForm);
			Pages.Incidents.FillForm(this.FormData);
			Pages.Incidents.SaveAndClose();
			Assert.IsTrue(Pages.Incidents.AlertSuccessExists());
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.GoToIncidentsPage();
			Pages.Incidents.GoToSearchForm();
			Assert.IsTrue(Pages.Incidents.IsAtSearchForm);
			Pages.Incidents.FillForm(this.FormData);
			Pages.Incidents.SaveAndClose();
			Assert.AreEqual(1, Pages.Incidents.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Incidents.SelectFirstTableEntity();
			Pages.Incidents.Delete();
			Assert.IsTrue(Pages.Incidents.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Incidents.TableRowsCount);
		}
	}
}