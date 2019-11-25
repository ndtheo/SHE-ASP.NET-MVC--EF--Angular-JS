namespace WebApplication.UITestFramework.Pages
{
    public class HomePage
    {
        public void Goto()
        {
            Pages.TopNavigation.HomeLink.Click();
        }

        public bool IsAt()
        {
            return Browser.WaitForTitle("Home");
        }
    }
}