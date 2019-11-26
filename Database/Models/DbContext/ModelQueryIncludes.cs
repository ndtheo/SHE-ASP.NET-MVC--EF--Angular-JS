#region Using Directives

using System.Data.Entity;
using System.Linq;
using Core.Entities;

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