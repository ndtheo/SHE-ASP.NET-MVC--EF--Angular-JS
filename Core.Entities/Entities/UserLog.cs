using System;
using System.ComponentModel.DataAnnotations;
using Utilities;
using Utilities.CustomJsonContractResolver;

namespace Core.Entities
{
	public class UserLog : BaseEntity
	{
		[Display(Name = "User")]
		public int UserID { get; set; }

		[Display(Name = "Login Date"), DataType(DataType.Date)]
		public DateTime DateLogin { get; set; }

		[Display(Name = "Logout Date"), DataType(DataType.Date)]
		public DateTime? DateLogout { get; set; }

		public string Ip { get; set; }

		[JsonGetOnly]
		public virtual User User { get; set; }
	}
}
