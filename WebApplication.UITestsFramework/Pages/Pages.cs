#region Using Directives

using OpenQA.Selenium.Support.PageObjects;

#endregion

namespace WebApplication.UITestFramework.Pages
{
    public static class Pages
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(Browser.Driver, page);
            return page;
        }

        public static HomePage Home => Pages.GetPage<HomePage>();
        public static LoginPage Login => Pages.GetPage<LoginPage>();
        public static ManageAccountPage ManageAccount => Pages.GetPage<ManageAccountPage>();
        public static MenuItemsPage MenuItems => Pages.GetPage<MenuItemsPage>();
        public static RegisterPage Register => Pages.GetPage<RegisterPage>();
        public static RightsPage Rights => Pages.GetPage<RightsPage>();
        public static RolesPage Roles => Pages.GetPage<RolesPage>();
        public static TopNavigationPage TopNavigation => Pages.GetPage<TopNavigationPage>();
        public static UsersPage Users => Pages.GetPage<UsersPage>();
    }
}