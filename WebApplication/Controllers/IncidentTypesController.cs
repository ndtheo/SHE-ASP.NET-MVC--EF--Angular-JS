using Core.Entities;
using Core.Logic.ExcelGenerators;
using Database.Models.Display;
using Database.Models.SearchCriteria;
using Database.Models.ViewModels;
using Database.Search;
using System.Web;
using System.Web.Mvc;
using WebApplication.Toolkit;
using WebApplication.Toolkit.BaseControllers;
using WebApplication.Toolkit.ExtensionMethods;
using WebApplication.Toolkit.Models;

namespace WebApplication.Controllers
{

    public class IncidentTypesController : BaseController
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
        public CustomJsonResult Search(IncidentTypeSearchCriteria criteria)
        {
            return this.CustomJson(this.SearchAndGetPage(criteria));
        }

        public string ExportGridToExcel(ExcelExportRequest<IncidentTypeSearchCriteria, DisplayWithName> excelExportRequest)
        {
            var path = $"{HttpRuntime.AppDomainAppPath}\\ExcelExport.xlsx";
            var data = this.SearchAndGetPage(excelExportRequest.SearchCriteria).Data;
            data.ToExcel(excelExportRequest.Display, path);
            return "ExcelExport.xlsx";
        }

        private PageViewModel<IncidentType> SearchAndGetPage(IncidentTypeSearchCriteria criteria)
        {
            var page = this.db.IncidentTypes.Search(criteria).GetPage(criteria);
            return page;
        }

        private void SetForeignKeys()
        {
        }
    }
}