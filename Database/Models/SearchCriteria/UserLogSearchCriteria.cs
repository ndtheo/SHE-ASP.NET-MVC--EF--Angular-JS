
#region Using Directives

using Core.Entities.SearchCriteria;
using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Database.Models.SearchCriteria
{
	public class UserLogSearchCriteria : BaseSearchCriteria
	{
		[Display(Name = "From User")]
		public int? UserIDFrom { get; set; }

		[Display(Name = "To User")]
		public int? UserIDTo { get; set; }

		[Display(Name = "FromDate Login")]
		public DateTime? DateLoginFrom { get; set; }

		[Display(Name = "To Date Login")]
		public DateTime? DateLoginTo { get; set; }

		[Display(Name = "FromDate Logout")]
		public DateTime? DateLogoutFrom { get; set; }

		[Display(Name = "To Date Logout")]
		public DateTime? DateLogoutTo { get; set; }

		public string Ip { get; set; }
	}
}

