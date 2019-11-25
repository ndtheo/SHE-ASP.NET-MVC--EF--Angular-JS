#region Using Directives

using System.Linq;
using Core.Entities;
using Database.Models.SearchCriteria;

#endregion

namespace Database.Search
{
    public static class UserSearch
    {
        public static IQueryable<User> Search(this IQueryable<User> users, UserSearchCriteria searchCriteria)
        {
            if (!string.IsNullOrWhiteSpace(searchCriteria.Email))
                users = users.Where(x => x.Email.Contains(searchCriteria.Email));
            if (searchCriteria.EmailConfirmed.HasValue)
                users = users.Where(x => x.EmailConfirmed == searchCriteria.EmailConfirmed);

            if (!string.IsNullOrWhiteSpace(searchCriteria.PasswordHash))
                users = users.Where(x => x.PasswordHash.Contains(searchCriteria.PasswordHash));

            if (!string.IsNullOrWhiteSpace(searchCriteria.SecurityStamp))
                users = users.Where(x => x.SecurityStamp.Contains(searchCriteria.SecurityStamp));

            if (!string.IsNullOrWhiteSpace(searchCriteria.PhoneNumber))
                users = users.Where(x => x.PhoneNumber.Contains(searchCriteria.PhoneNumber));

            if (searchCriteria.PhoneNumberConfirmed.HasValue)
                users = users.Where(x => x.PhoneNumberConfirmed == searchCriteria.PhoneNumberConfirmed);

            if (searchCriteria.TwoFactorEnabled.HasValue)
                users = users.Where(x => x.TwoFactorEnabled == searchCriteria.TwoFactorEnabled);

            if (searchCriteria.LockoutEndDateUtcFrom.HasValue)
                users = users.Where(x => x.LockoutEndDateUtc.HasValue && x.LockoutEndDateUtc >= searchCriteria.LockoutEndDateUtcFrom);

            if (searchCriteria.LockoutEndDateUtcTo.HasValue)
                users = users.Where(x => x.LockoutEndDateUtc.HasValue && x.LockoutEndDateUtc <= searchCriteria.LockoutEndDateUtcTo);

            if (searchCriteria.LockoutEnabled.HasValue)
                users = users.Where(x => x.LockoutEnabled == searchCriteria.LockoutEnabled);

            if (searchCriteria.AccessFailedCountFrom.HasValue)
                users = users.Where(x => x.AccessFailedCount >= searchCriteria.AccessFailedCountFrom);

            if (searchCriteria.AccessFailedCountTo.HasValue)
                users = users.Where(x => x.AccessFailedCount <= searchCriteria.AccessFailedCountTo);

            if (!string.IsNullOrWhiteSpace(searchCriteria.UserName))
                users = users.Where(x => x.UserName.Contains(searchCriteria.UserName));
            if (!string.IsNullOrWhiteSpace(searchCriteria.RoleId))
                users = users.Where(x => x.Roles.Select(r => r.RoleId).Contains(searchCriteria.RoleId));
            if (!string.IsNullOrEmpty(searchCriteria.Name))
                users = users.Where(x => x.Name.Contains(searchCriteria.Name));

            return users;
        }
    }
}