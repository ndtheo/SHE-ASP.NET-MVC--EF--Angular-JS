#region Using Directives

using Core.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication.BaseControllers;

#endregion

namespace WebApplication.Api_Controllers
{
    public class IncidentTypesController : BaseApiController
	{
		// GET api/AccidentTypes
		public IQueryable<IncidentType> GetAccidentTypes() => this.db.IncidentTypes;

		// GET api/AccidentTypes/5
		[ResponseType(typeof(IncidentType))]
		public IHttpActionResult GetIncidentType(int id)
		{
			var accidenttype = this.db.IncidentTypes.Find(id);

			if (accidenttype == null)
				return this.NotFound();

			return this.Ok(accidenttype);
		}

		// PUT api/AccidentTypes/5
		public IHttpActionResult PutAccidentType(int id, IncidentType accidenttype)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest(this.ModelState);

			if (id != accidenttype.Id)
				return this.BadRequest();

			this.db.Entry(accidenttype).State = EntityState.Modified;

			try
			{
				this.db.SaveChanges(this.UserId);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!this.AccidentTypeExists(id))
					return this.NotFound();
				throw;
			}

			return this.StatusCode(HttpStatusCode.NoContent);
		}

		// POST api/AccidentTypes
		[ResponseType(typeof(IncidentType))]
		public IHttpActionResult PostAccidentType(IncidentType incidenttype)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest(this.ModelState);

			this.db.IncidentTypes.Add(incidenttype);
			this.db.SaveChanges(this.UserId);

			return this.CreatedAtRoute("DefaultApi", new { id = incidenttype.Id }, incidenttype);
		}

		// DELETE api/AccidentTypes/5
		[ResponseType(typeof(IncidentType))]
		public IHttpActionResult DeleteAccidentType(int id)
		{
			var accidenttype = this.db.IncidentTypes.Find(id);

			if (accidenttype == null)
				return this.NotFound();

			this.db.IncidentTypes.Remove(accidenttype);
			this.db.SaveChanges(this.UserId);

			return this.Ok(accidenttype);
		}

		private bool AccidentTypeExists(int id) => this.db.IncidentTypes.Count(e => e.Id == id) > 0;
	}
}