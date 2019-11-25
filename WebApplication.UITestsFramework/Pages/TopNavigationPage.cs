#region Using Directives

using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

#endregion

namespace WebApplication.UITestFramework.Pages
{
    public class TopNavigationPage
    {
        #region Top Navigation Links 

        [FindsBy(How = How.CssSelector, Using = "navbar-brand")] public IWebElement HomeLink;
        [FindsBy(How = How.LinkText, Using = "Register")] public IWebElement RegisterLink;
        [FindsBy(How = How.LinkText, Using = "Log in")] public IWebElement LogInLink;
        [FindsBy(How = How.LinkText, Using = "Log off")] public IWebElement LogOutLink;
        [FindsBy(How = How.Id, Using = "ManageLink")] public IWebElement ManageLink;

        #endregion

        #region Dropdown Menus

        [FindsBy(How = How.LinkText, Using = "CRM")] public IWebElement CrmDropdown;
        [FindsBy(How = How.LinkText, Using = "Inventory")] public IWebElement InventoryDropdown;
        [FindsBy(How = How.LinkText, Using = "Accounting")] public IWebElement AccountingDropdown;
        [FindsBy(How = How.LinkText, Using = "Administration")] public IWebElement AdministrationDropdown;
        [FindsBy(How = How.LinkText, Using = "Reports")] public IWebElement ReportsDropdown;

        #endregion

        #region CRM Navigation Links

        [FindsBy(How = How.LinkText, Using = "Activities")] public IWebElement ActivitiesLink;
        [FindsBy(How = How.LinkText, Using = "Ad Units")] public IWebElement AdUnitsLink;
        [FindsBy(How = How.LinkText, Using = "Billing Categories")] public IWebElement BillingCategoriesLink;
        [FindsBy(How = How.LinkText, Using = "Calendar")] public IWebElement CalendarLink;
        [FindsBy(How = How.LinkText, Using = "Clients")] public IWebElement ClientsLink;
        [FindsBy(How = How.LinkText, Using = "Contacts")] public IWebElement ContactsLink;
        [FindsBy(How = How.LinkText, Using = "Countries")] public IWebElement CountriesLink;
        [FindsBy(How = How.LinkText, Using = "Credit Policies")] public IWebElement CreditPoliciesLink;
        [FindsBy(How = How.LinkText, Using = "Credit Ratings")] public IWebElement CreditRatingsLink;
        [FindsBy(How = How.LinkText, Using = "Display Types")] public IWebElement DisplayTypesLink;
        [FindsBy(How = How.LinkText, Using = "ERP Projects")] public IWebElement ErpProjectsLink;
        [FindsBy(How = How.LinkText, Using = "Exposure Types")] public IWebElement ExposureTypesLink;
        [FindsBy(How = How.LinkText, Using = "Forecasts")] public IWebElement ForecastsLink;
        [FindsBy(How = How.LinkText, Using = "Formats")] public IWebElement FormatsLink;
        [FindsBy(How = How.LinkText, Using = "Invoice Items")] public IWebElement InvoiceItemsLink;
        [FindsBy(How = How.LinkText, Using = "Invoices")] public IWebElement InvoicesLink;
        [FindsBy(How = How.LinkText, Using = "Master Groups")] public IWebElement MasterGroupsLink;
        [FindsBy(How = How.LinkText, Using = "Menu Items")] public IWebElement MenuItemsLink;
        [FindsBy(How = How.LinkText, Using = "Offer Items")] public IWebElement OfferItemsLink;
        [FindsBy(How = How.LinkText, Using = "Offers")] public IWebElement OffersLink;
        [FindsBy(How = How.LinkText, Using = "Order Items")] public IWebElement OrderItemsLink;
        [FindsBy(How = How.LinkText, Using = "Orders")] public IWebElement OrdersLink;
        [FindsBy(How = How.LinkText, Using = "Placements")] public IWebElement PlacementsLink;
        [FindsBy(How = How.LinkText, Using = "Platforms")] public IWebElement PlatformsLink;
        [FindsBy(How = How.LinkText, Using = "Product Categories")] public IWebElement ProductCategoriesLink;
        [FindsBy(How = How.LinkText, Using = "Products")] public IWebElement ProductsLink;
        [FindsBy(How = How.LinkText, Using = "Rights")] public IWebElement RightsLink;
        [FindsBy(How = How.LinkText, Using = "Roles")] public IWebElement RolesLink;
        [FindsBy(How = How.LinkText, Using = "Sizes")] public IWebElement SizesLink;
        [FindsBy(How = How.LinkText, Using = "Tax Offices")] public IWebElement DoisLink;
        [FindsBy(How = How.LinkText, Using = "Users")] public IWebElement UsersLink;
        [FindsBy(How = How.LinkText, Using = "Websites")] public IWebElement WebsitesLink;
        #endregion

        #region Top Navigation Links Usage

        public void Home()
        {
            this.HomeLink.Click();
        }

        public bool IsLoggedIn => this.ManageLink.Exists();

        public void LogOut()
        {
            this.LogOutLink.Click();
        }

        public void LogIn()
        {
            this.LogInLink.Click();
        }

        #endregion
    }
}