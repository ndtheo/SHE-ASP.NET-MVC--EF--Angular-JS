#region Using Directives

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Reflection;
using Core.Entities;
using System.Data.Entity.Validation;

#endregion

namespace Database.Models.DbContext
{
    public  partial class IncidentsDataContext : IdentityDbContext<User> //Microsoft.EntityFrameworkCore.DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(
        //    //    @"Server=localhost;Database=PrimeXM;Integrated Security=True");
        //}
    }

    public partial class IncidentsDataContext : IdentityDbContext<User>
	{
		public IncidentsDataContext() : base("DefaultConnection", false)
		{
			System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<IncidentsDataContext, Migrations.Configuration>());
		}

        // App tables
        public DbSet<IncidentType> IncidentTypes { get; set; }
        public DbSet<Incident> Incidents { get; set; }


        public DbSet<UserLog> UserLogs { get; set; }

        public static IncidentsDataContext Create()
		{
			return new IncidentsDataContext();
		}

		/// <summary>
		///     Saves all changes made in this context to the underlying database.
		/// </summary>
		/// <returns>
		///     The number of state entries written to the underlying database. This can include
		///     state entries for entities and/or relationships. Relationship state entries are created for
		///     many-to-many relationships and relationships where there is no foreign key property
		///     included in the entity class (often referred to as independent associations).
		/// </returns>
		public int SaveChanges(string userId = null)
		{
			foreach (var dbEntityEntry in this.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added))
			{
				var entry = dbEntityEntry.Entity as BaseEntity;

				if (entry == null) continue;

				if (dbEntityEntry.State != EntityState.Added) continue;
            }
            int changesCount = 0;
         
            var changedEntries = this.ChangeTracker.Entries().Select(x => x.Entity).ToList();
            try {
                 changesCount = base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
         
            return changesCount;
		}
	}
}