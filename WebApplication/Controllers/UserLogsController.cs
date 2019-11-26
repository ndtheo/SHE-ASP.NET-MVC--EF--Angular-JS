#region Using Directives

using Core.Entities;
using Database.Models.SearchCriteria;
using Database.Models.ViewModels;
using Database.Search;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Toolkit.BaseControllers;
using WebApplication.Toolkit;
using WebApplication.Toolkit.ExtensionMethods;
using WebApplication.Toolkit.Security;

#endregion

namespace WebApplication.Controllers
{
    [RequireRights(View = true)]
	public class UserLogsController : BaseController
	{
		public ActionResult Index(bool isTab = false)
		{
			this.ViewBag.IsTab = isTab;
			return this.View();
		}
 
		public ActionResult Details(bool editMode = false)
		{
			this.SetForeignKeys();
			this.ViewBag.EditMode = editMode;
			return this.View();
		}

		public ActionResult SearchCriteria()
		{
			this.SetForeignKeys();
			return this.View();
		}

		[HttpPost]
		public CustomJsonResult Search(UserLogSearchCriteria criteria)
		{
			return this.CustomJson(this.SearchAndGetPage(criteria));
		}

		private PageViewModel<UserLog> SearchAndGetPage(UserLogSearchCriteria criteria)
		{
			//this.db.Configuration.LazyLoadingEnabled = false;
			var page = this.db.UserLogs.Search(criteria).GetPage(criteria);
			return page;
		}

		private void SetForeignKeys()
		{
			this.ViewBag.CreatorId = new SelectList(this.db.Users.OrderBy(x=>x.Name), "Id", "Name");
			this.ViewBag.LastUpdateUserId = new SelectList(this.db.Users.OrderBy(x=>x.Name), "Id", "Name");
		}
	}
}