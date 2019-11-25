#region Using Directives

using Core.Entities;
using Database.Models.SearchCriteria;
using System.Linq;

#endregion

namespace Database.Search
{
    public static class IncidentTypeSearch
	{
		public static IQueryable<IncidentType> Search(this IQueryable<IncidentType> incidenttypes, IncidentTypeSearchCriteria searchCriteria)
		{
			if(!string.IsNullOrWhiteSpace(searchCriteria.Name))
			{
				incidenttypes = incidenttypes.Where(x => x.Name.Contains(searchCriteria.Name));
			}
			return  incidenttypes;
		}
	}
}

