#region Using Directives

using Core.Entities;
using Database.Models.DbContext;
using System;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;

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

//        For delivery
//To recheck
//Proposal without charge
//Proposal for Unobtrusive
//Simple transition


    }
}