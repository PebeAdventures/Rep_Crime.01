namespace Rep_Crime._01_Crime.API.Models
{
    public class CrimeEvent
    {
        public int Id { get; set; }
        public EventType EventType { get; set; }

        public string Description { get; set; }
        public string PlaceOfEvent { get; set; }
        public string ReportingPersonEmail { get; set; }
        public EventStatus EventStatus { get; set; }
        public string AssigneLawEnforcementID { get; set; }


    }
}
