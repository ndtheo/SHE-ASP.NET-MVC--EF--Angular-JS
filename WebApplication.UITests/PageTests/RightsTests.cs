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
	public class RightsTests : TestBase
	{
		private Dictionary<string, string> FormData { get; } = new Dictionary<string, string>{};
		private Dictionary<string, Dictionary<string, string>> FormDropdownData { get; } = new Dictionary<string, Dictionary<string, string>>
		{
			["RoleId"] = new Dictionary<string, string> { ["Name"] = "Admin" },
			["MenuItemId"] = new Dictionary<string, string> { ["Name"] = "Activities" },
		};

		[Test, Order(1)]
		public void GoToRightsPage()
		{
			Pages.Rights.Goto();
			Assert.IsTrue(Pages.Rights.IsAt);
		}

		[Test, Order(2)]
		public void CreateNewRight()
		{
			this.GoToRightsPage();
			Pages.Rights.GoToAddNewForm();
			Assert.IsTrue(Pages.Rights.IsAtDetailsForm);
			Pages.Rights.FillForm(this.FormData, this.FormDropdownData);
			Pages.Rights.SaveAndClose();
			Assert.IsTrue(Pages.Rights.AlertSuccessExists());
		}

		[Test, Order(3)]
		public void TestUniqueRightNameCheck()
		{
			this.GoToRightsPage();
			Pages.Rights.GoToAddNewForm();
			Assert.IsTrue(Pages.Rights.IsAtDetailsForm);
			Pages.Rights.FillForm(this.FormData, this.FormDropdownData);
			Assert.IsTrue(Pages.Rights.UniqueNameMessageVisible());
			Pages.Rights.CloseModal();
		}

		[Test, Order(4)]
		public void SearchCriteriaWorks()
		{
			this.FormDropdownData.Add("View", new Dictionary<string, string> { ["Name"] = "False" });

			this.GoToRightsPage();
			Pages.Rights.GoToSearchForm();
			Assert.IsTrue(Pages.Rights.IsAtSearchForm);
			Pages.Rights.FillForm(this.FormData, this.FormDropdownData);
			Pages.Rights.SaveAndClose();
			Assert.AreEqual(1, Pages.Rights.TableRowsCount);
		}

		[Test, Order(5)]
		public void TestDeleteEntity()
		{
			Pages.Rights.SelectFirstTableEntity();
			Pages.Rights.Delete();
			Assert.IsTrue(Pages.Rights.AlertSuccessExists());
			Assert.AreEqual(0, Pages.Rights.TableRowsCount);
		}
	}
}