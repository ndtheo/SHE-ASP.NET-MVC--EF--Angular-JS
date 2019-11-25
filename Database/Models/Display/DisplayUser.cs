namespace Database.Models.Display
{
    public class DisplayUser
    {
        public bool Name { get; set; }
        public bool Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PasswordHash { get; set; }
        public bool SecurityStamp { get; set; }
        public bool PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool AccessFailedCount { get; set; }
        public bool UserName { get; set; }
    }
}