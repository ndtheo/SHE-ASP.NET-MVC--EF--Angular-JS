using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities.SearchCriteria
{
    public class IncidentSearchCriteria : BaseSearchCriteria
    {
        [Display(Name = "Incident Type")]
        public int? IncidentTypeId { get; set; }

        [Display(Name = "Incident Date From")]
        public DateTime? IncidentDateFrom { get; set; }

        [Display(Name = "Incident Date To")]
        public DateTime? IncidentDateTo { get; set; }

        [Display(Name = "Incident Time From")]
        public DateTime? IncidentTimeFrom { get; set; }

        [Display(Name = "Incident Time To")]
        public DateTime? IncidentTimeTo { get; set; }
        public string Description { get; set; }
        public string Person { get; set; }
    }
}
