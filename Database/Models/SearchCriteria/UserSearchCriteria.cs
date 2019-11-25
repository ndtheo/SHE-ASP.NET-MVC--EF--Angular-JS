#region Using Directives

using Core.Entities.SearchCriteria;
using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Database.Models.SearchCriteria
{
    public class UserSearchCriteria : BaseSearchCriteria
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtcFrom { get; set; }
        public DateTime? LockoutEndDateUtcTo { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCountFrom { get; set; }
        public int? AccessFailedCountTo { get; set; }
        public string RoleId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
}