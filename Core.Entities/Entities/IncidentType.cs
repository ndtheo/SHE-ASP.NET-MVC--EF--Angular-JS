using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class IncidentType : BaseEntity
    {
        [Display(Name = EntityLabels.INCIDENT_TYPE)]
        public string Name { get; set; }
    }
}
