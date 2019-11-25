#region Using Directives

using WebApplication.UITestFramework.Models;

#endregion

namespace WebApplication.UITestFramework.Generators
{
    public static class UserGenerator
    {
        public static User LastGeneratedUser;

        public static void Initialize()
        {
            UserGenerator.LastGeneratedUser = null;
        }

        public static User Generate()
        {
            var user = new User
            {
                EmailAddress = EmailAddressGenerator.Generate(),
                Password = PasswordGenerator.Generate()
            };

            UserGenerator.LastGeneratedUser = user;
            return user;
        }
    }
}