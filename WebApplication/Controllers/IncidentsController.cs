#region Using Directives

using Core.Entities;
using Core.Entities.SearchCriteria;
using Core.Logic.ExcelGenerators;
using Database.Models.Display;
using Database.Models.ViewModels;
using Database.Search;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Toolkit.BaseControllers;
using WebApplication.VisionToolkit;
using WebApplication.VisionToolkit.ExtensionMethods;
using WebApplication.VisionToolkit.Models;
using WebApplication.VisionToolkit.Security;

#endregion

namespace WebApplication.Controllers
{
    [RequireRights(View = true)]
	public class IncidentsController : BaseController
	{
		public ActionResult Index(bool isTab = false,bool pendingAccidents=false)
		{
			this.ViewBag.IsTab = isTab;
            return this.View();
		}

		public ActionResult Details(bool editMode = false)
		{
			this.SetForeignKeys();
			this.ViewBag.EditMode = editMode;
            if (!editMode)
            {
               
            }
			return this.View();
		}

		public ActionResult SearchCriteria()
		{
			this.SetForeignKeys();
			return this.View();
		}

		[HttpPost]
		public CustomJsonResult Search(IncidentSearchCriteria criteria)
		{
			return this.CustomJson(this.SearchAndGetPage(criteria));
		}

		public string ExportGridToExcel(ExcelExportRequest<IncidentSearchCriteria, DisplayIncident> excelExportRequest)
		{
			var path = $"{HttpRuntime.AppDomainAppPath}\\ExcelExport.xlsx";
			var data = this.SearchAndGetPage(excelExportRequest.SearchCriteria).Data;
			data.ToExcel(excelExportRequest.Display, path);
			return "ExcelExport.xlsx";
		}

        [HttpGet]
        public Incident GetIncident(int id)
        {
            var incident = this.db.Incidents.Find(id);
            if (incident is null) throw new Exception("Incident not found");

            return incident;
        }

        [HttpGet]
        public CustomJsonResult GetAllIncidents()
        {
            var incidents = this.db.Incidents.Select(a => new
            {
                a.Id
            });
            return this.CustomJson(incidents);
        }

        private PageViewModel<Incident> SearchAndGetPage(IncidentSearchCriteria criteria)
		{
            var page = this.db.Incidents.Search(criteria).GetPage(criteria);
			return page;
		}

        private void SetForeignKeys()
		{
			this.ViewBag.IncidentTypesId = new SelectList(this.db.IncidentTypes.OrderBy(x => x.Name), "Id", "Name");
			//this.ViewBag.CreatorId = new SelectList(this.db.Users.OrderBy(x => x.Name), "Id", "Name");
			this.ViewBag.LastUpdateUserId = new SelectList(this.db.Users.OrderBy(x => x.Name), "Id", "Name");        }
    }
}