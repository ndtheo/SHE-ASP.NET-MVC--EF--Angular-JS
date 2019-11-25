#region Using Directives

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

#endregion

namespace Database.Models.DbContext
{
    public partial class IncidentsDataContext
	{
		/// <summary>
		///     Maps table names, and sets up relationships between the various user entities, Entity framework Fluent API
		/// </summary>
		/// <param name="modelBuilder" />
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			modelBuilder.Properties<string>().Where(x => x.Name == "Name").Configure(p => p.HasMaxLength(256));
			

			base.OnModelCreating(modelBuilder);
		}
	}

	public static class MappingExtensions
	{
		public static PrimitivePropertyConfiguration IsUnique(this PrimitivePropertyConfiguration configuration)
		{
			return configuration.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute
			{
				IsUnique = true
			}));
		}
	}
}