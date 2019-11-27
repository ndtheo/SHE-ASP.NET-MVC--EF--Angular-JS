namespace Database.Models.Display
{
    public class DisplayIncident 
	{
        public DisplayIncident()
        {
            this.IncidentDate = true;
            this.IncidentTime = true;
            this.IncidentType = true;
            this.Person = true;
            this.Description = true;
            this.Location = true;
        }
		public bool IncidentType { get; set; }
		public bool IncidentDate { get; set; }
		public bool IncidentTime { get; set; }
        public bool Description { get; set; }
        public bool Person { get; set; }

        public bool Location { get; set; }
    }
}
