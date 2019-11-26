#region Using Directives

using Core.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication.BaseControllers;
using WebApplication.Toolkit.Security;

#endregion

namespace WebApplication.Api_Controllers
{
    public class UsersController : BaseApiController
    {
        // GET api/Users
        public IQueryable<User> GetUsers() => this.db.Users;

        // GET api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            var user = this.db.Users.Find(id);

            if (user == null)
                return this.NotFound();

            return this.Ok(user);
        }

        // PUT api/Users/5
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            if (id != user.Id)
                return this.BadRequest();

            this.db.Entry(user).State = EntityState.Modified;

            try
            {
                this.db.SaveChanges(this.UserId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.UserExists(id))
                    return this.NotFound();
                throw;
            }

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            this.db.Users.Add(user);
            this.db.SaveChanges(this.UserId);

            return this.CreatedAtRoute("DefaultApi", new
            {
                id = user.Id
            }, user);
        }

        // DELETE api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            var user = this.db.Users.Find(id);

            if (user == null)
                return this.NotFound();

            this.db.Users.Remove(user);
            this.db.SaveChanges(this.UserId);

            return this.Ok(user);
        }

        private bool UserExists(string id) => this.db.Users.Count(e => e.Id == id) > 0;
    }
}