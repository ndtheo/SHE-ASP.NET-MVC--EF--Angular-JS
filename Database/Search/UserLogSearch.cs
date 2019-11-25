#region Using Directives

using System.Linq;
using Core.Entities;
using Database.Models.DbContext;
using Database.Models.SearchCriteria;

#endregion

namespace Database.Search
{
	public static class UserLogSearch
	{
		public static IQueryable<UserLog> Search(this IQueryable<UserLog> userlogs, UserLogSearchCriteria searchCriteria)
		{
			if(searchCriteria.UserIDFrom.HasValue)
			{
				userlogs = userlogs.Where(x => x.UserID >= searchCriteria.UserIDFrom);
			}

			if(searchCriteria.UserIDTo.HasValue)
			{
				userlogs = userlogs.Where(x => x.UserID <= searchCriteria.UserIDTo);
			}

			if(searchCriteria.DateLoginFrom.HasValue)
			{
				userlogs = userlogs.Where(x => x.DateLogin >= searchCriteria.DateLoginFrom);
			}

            if (searchCriteria.DateLoginTo.HasValue)
			{
				userlogs = userlogs.Where(x => x.DateLogin <= searchCriteria.DateLoginTo);
			}

			if(searchCriteria.DateLogoutFrom.HasValue)
			{
				userlogs = userlogs.Where(x => x.DateLogout >= searchCriteria.DateLogoutFrom);
			}

			if(searchCriteria.DateLogoutTo.HasValue)
			{
				userlogs = userlogs.Where(x => x.DateLogout <= searchCriteria.DateLogoutTo);
			}

			if(!string.IsNullOrWhiteSpace(searchCriteria.Ip))
			{
				userlogs = userlogs.Where(x => x.Ip.Contains(searchCriteria.Ip));
			}
			return  userlogs;
		}
	}
}

