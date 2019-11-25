#region Using Directives

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Entities;
using Core.Logic.ExcelGenerators;
using Database.Models.Display;
using Database.Models.SearchCriteria;
using Database.Models.ViewModels;
using Database.Search;
using WebApplication.Toolkit.BaseControllers;
using WebApplication.VisionToolkit;
using WebApplication.VisionToolkit.ExtensionMethods;
using WebApplication.VisionToolkit.Models;
using WebApplication.VisionToolkit.Security;

#endregion

namespace WebApplication.Controllers
{
    [RequireRights(View = true)]
    public class UsersController : BaseController
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
        public CustomJsonResult Search(UserSearchCriteria criteria)
        {
            return this.CustomJson(this.SearchAndGetPage(criteria));
        }

        public ActionResult AddExisting(string id)
        {
            // Select from the users tha are not in this role
            this.ViewBag.Id = new SelectList(this.db.Users.Where(r => !r.Roles.Select(u => u.RoleId).Contains(id)).OrderBy(x => x.Email), "Id", "Name");
            return this.View();
        }

        private PageViewModel<User> SearchAndGetPage(UserSearchCriteria criteria)
        {
            //this.db.Configuration.LazyLoadingEnabled = false;
            var page = this.db.Users.Search(criteria).GetPage(criteria);
            return page;
        }

        private void SetForeignKeys() {}
    }
}