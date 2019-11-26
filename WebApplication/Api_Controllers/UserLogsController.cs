#region Using Directives

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Core.Entities;
using WebApplication.BaseControllers;
using WebApplication.Toolkit.Security;

#endregion

namespace WebApplication.Api_Controllers
{
	public class UserLogsController : BaseApiController
	{
		// GET api/UserLogs
		public IQueryable<UserLog> GetUserLogs() => this.db.UserLogs;

		// GET api/UserLogs/5
		[ResponseType(typeof(UserLog))]
		public IHttpActionResult GetUserLog(int id)
		{
			var userlog = this.db.UserLogs.Find(id);

			if (userlog == null)
				return this.NotFound();

			return this.Ok(userlog);
		}

		// PUT api/UserLogs/5
		public IHttpActionResult PutUserLog(int id, UserLog userlog)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest(this.ModelState);

			if (id != userlog.Id)
				return this.BadRequest();

			this.db.Entry(userlog).State = EntityState.Modified;

			try
			{
				this.db.SaveChanges(this.UserId);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!this.UserLogExists(id))
					return this.NotFound();
				throw;
			}

			return this.StatusCode(HttpStatusCode.NoContent);
		}

		// POST api/UserLogs
		[ResponseType(typeof(UserLog))]
		public IHttpActionResult PostUserLog(UserLog userlog)
		{
			if (!this.ModelState.IsValid)
				return this.BadRequest(this.ModelState);

			this.db.UserLogs.Add(userlog);
			this.db.SaveChanges(this.UserId);

			return this.CreatedAtRoute("DefaultApi", new { id = userlog.Id }, userlog);
		}

		// DELETE api/UserLogs/5
		[ResponseType(typeof(UserLog))]
		public IHttpActionResult DeleteUserLog(int id)
		{
			var userlog = this.db.UserLogs.Find(id);

			if (userlog == null)
				return this.NotFound();

			this.db.UserLogs.Remove(userlog);
			this.db.SaveChanges(this.UserId);

			return this.Ok(userlog);
		}

		private bool UserLogExists(int id) => this.db.UserLogs.Count(e => e.Id == id) > 0;
	}
}