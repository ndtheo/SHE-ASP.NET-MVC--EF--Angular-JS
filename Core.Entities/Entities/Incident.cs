using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.CustomJsonContractResolver;

namespace Core.Entities
{
    /// <summary>
    /// Base Entity (<see cref="BaseEntity"/>) contains a primary key by default
    /// </summary>C:\Users\Andreas\Documents\SHE_MVC\Core.Entities\Entities\Incident.cs
    public class Incident : BaseEntity
    {
        [Display(Name = "Incident Type"), Required]
        public int IncidentTypeId { get; set; }

        [ForeignKey("IncidentTypeId"), JsonGetOnly]
        public IncidentType IncidentType { get; set; }


        [Display(Name = "Incident Date")]
        public DateTime IncidentDate { get; set; }

        [Display(Name = "Incident Time")]
        public DateTime IncidentTime { get; set; }

        public string Description { get; set; }

        public string Person { get; set; }

        public string Location { get; set; }
    }
}

//•	Type of Incident – list of different kinds of incidents
//•	Date of incident - date
//•	Time of incident - time
//•	Description of event - string
//•	(Person) Who was involved - string

