using System;

namespace Core.Entities.SearchCriteria
{
    public class IncidentSearchCriteria : BaseSearchCriteria
    {
        public int? IncidentTypeId { get; set; }
        public DateTime? IncidentDateFrom { get; set; }
        public DateTime? IncidentDateTo { get; set; }
        public DateTime? IncidentTimeFrom { get; set; }
        public DateTime? IncidentTimeTo { get; set; }
        public string Description { get; set; }
        public string Person { get; set; }
    }
}
