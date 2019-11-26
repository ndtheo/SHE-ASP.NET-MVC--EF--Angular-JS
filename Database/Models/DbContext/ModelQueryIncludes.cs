#region Using Directives

using Core.Entities;
using System.Data.Entity;
using System.Linq;

#endregion

namespace Database.Models.DbContext
{
    public static class ModelQueryIncludes
    {
        public static IQueryable<Incident> IncludeAll(this IQueryable<Incident> incident)
        {
            incident = incident
                .Include(x => x.IncidentType);
            return incident;
        }

    }
}