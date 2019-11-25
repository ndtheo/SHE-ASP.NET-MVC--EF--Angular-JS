#region Using Directives

using System.Threading;
using NUnit.Framework;
using WebApplication.UITestFramework.Enums;
using WebApplication.UITestFramework.Generators;

#endregion

namespace WebApplication.UITestFramework
{
    [TestFixture]
    public class TestBase
    {
        [OneTimeSetUp]
        public static void Initialize()
        {
            Browser.Initialize();
            UserGenerator.Initialize();

            Pages.Pages.Login.Goto();
            Pages.Pages.Login.LogIn(LoginOptions.PredefinedCredentials);
            Assert.IsTrue(Pages.Pages.TopNavigation.IsLoggedIn);
        }

        [OneTimeTearDown]
        public static void TestFixtureTearDown()
        {
            Browser.Close();
        }

        [SetUp]
        public static void SetUp() {}

        [TearDown]
        public static void TearDown()
        {
            Thread.Sleep(200);
        }
    }
}