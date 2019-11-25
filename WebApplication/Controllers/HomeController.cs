#region Using Directives

using System.Web.Mvc;
using WebApplication.Toolkit.BaseControllers;

#endregion

namespace WebApplication.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index() => this.View();
    }
}