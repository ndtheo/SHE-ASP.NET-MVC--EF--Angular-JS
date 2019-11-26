#region Using Directives

using Core.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication.BaseControllers;
using WebApplication.Toolkit.Security;

#endregion

namespace WebApplication.Api_Controllers
{
	public class IncidentsController : BaseApiController
	{
		// GET api/Accidents
		public IQueryable<Incident> GetIncidents() => this.db.Incidents;

		// GET api/Accidents/5
		[ResponseType(typeof(Incident))]
		public IHttpActionResult GetIncident(int id)
		{
			var accident = this.db.Incidents.Find(id);

			if (accident == null)
				return this.NotFound();

			return this.Ok(accident);
		}


        private string GetValidationErrorMessage(Incident incident)
        {
            string errorMessage = string.Empty;
            if (null != incident)
            {
                var lowerLimit = new DateTime(1950, 1, 1);
                if (incident.IncidentDate< lowerLimit)
                {
                    errorMessage += " Incident Date is invalid.";
                }
            }
            else
            {
                Debug.Assert(false);
            }
            return errorMessage;
        }

        private void validateIncident(Incident accident)
        {
            string errorMessage = this.GetValidationErrorMessage(accident);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new System.Exception(errorMessage);
            }
        }


        // PUT api/Incidents/5, Update
		public IHttpActionResult PutIncident(int id, Incident incident)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest(this.ModelState);

			if (id != incident.Id)
				return this.BadRequest();

			this.db.Entry(incident).State = EntityState.Modified;

            this.validateIncident(incident);
			try
			{
                this.db.SaveChanges(this.UserId);               
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!this.IncidentExists(id))
					return this.NotFound();
				throw;
			}

			return this.StatusCode(HttpStatusCode.NoContent);
		}

		// POST api/Accidents, Create
		[ResponseType(typeof(Incident))]
		public IHttpActionResult PostIncident(Incident accident)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest(this.ModelState);

            this.validateIncident(accident);
            this.db.Incidents.Add(accident);
            this.db.SaveChanges(this.UserId);

			return this.CreatedAtRoute("DefaultApi", new { id = accident.Id }, accident);
		}

		// DELETE api/Accidents/5
		[ResponseType(typeof(Incident))]
		public IHttpActionResult DeleteIncident(int id)
		{
			var incident = this.db.Incidents.Find(id);

			if (incident == null)
				return this.NotFound();

            this.db.Incidents.Remove(incident);
			this.db.SaveChanges(this.UserId);

			return this.Ok(incident);
		}

		private bool IncidentExists(int id) => this.db.Incidents.Count(e => e.Id == id) > 0;
	}
}