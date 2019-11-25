namespace WebApplication.UITestFramework.Generators
{
    public static class PasswordGenerator
    {
        private static bool toggle = true;

        public static string Generate()
        {
            var password = "";
            password = PasswordGenerator.toggle ? "Password" : "New Password";

            PasswordGenerator.toggle = !PasswordGenerator.toggle;
            PasswordGenerator.LastGeneratedPassword = password;
            return password;
        }

        public static string LastGeneratedPassword { get; set; }
    }
}