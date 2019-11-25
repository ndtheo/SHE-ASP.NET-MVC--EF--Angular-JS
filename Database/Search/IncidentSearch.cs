#region Using Directives

using Core.Entities;
using Core.Entities.SearchCriteria;
using System.Linq;

#endregion

namespace Database.Search
{
    public static class IncidentSearch
	{
		public static IQueryable<Incident> Search(this IQueryable<Incident> incidents, IncidentSearchCriteria searchCriteria)
		{
			if(searchCriteria.IncidentTypeId.HasValue)
			{
				incidents = incidents.Where(x => x.IncidentTypeId == searchCriteria.IncidentTypeId);
			}
            if (searchCriteria.IncidentDateFrom.HasValue)
			{
				incidents = incidents.Where(x => x.IncidentDate >= searchCriteria.IncidentDateFrom);
			}
			if(searchCriteria.IncidentDateTo.HasValue)
			{
				incidents = incidents.Where(x => x.IncidentDate <= searchCriteria.IncidentDateTo);
			}
			if(!string.IsNullOrWhiteSpace(searchCriteria.Description))
			{
				incidents = incidents.Where(x => x.Description.Contains(searchCriteria.Description));
			}
            if (!string.IsNullOrWhiteSpace(searchCriteria.Person))
            {
                incidents = incidents.Where(x => x.Person.Contains(searchCriteria.Person));
            }
            return  incidents;
		}
	}
}

