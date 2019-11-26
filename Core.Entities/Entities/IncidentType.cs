using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class IncidentType : BaseEntity
    {
        [Display(Name = EntityLabels.INCIDENT_TYPE)]
        public new string Name { get; set; }
    }
}
