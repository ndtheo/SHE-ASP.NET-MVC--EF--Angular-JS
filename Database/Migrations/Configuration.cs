#region Using Directives

using Database.Models.DbContext;
using System.Data.Entity.Migrations;

#endregion

namespace Database.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<IncidentsDataContext>
	{
		public Configuration()
		{
			this.AutomaticMigrationsEnabled = true;
			this.AutomaticMigrationDataLossAllowed = true;
			this.ContextKey = "WebApplication.Migrations.Configuration";
		}
    }
}