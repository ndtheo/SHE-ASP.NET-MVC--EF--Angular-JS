
#region Using Directives

using Core.Entities;
using Core.Entities.SearchCriteria;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Database.Models.SearchCriteria
{
    public class IncidentTypeSearchCriteria : BaseSearchCriteria
	{
        [Display(Name = EntityLabels.INCIDENT_TYPE)]
        public string Name { get; set; }
	}
}
