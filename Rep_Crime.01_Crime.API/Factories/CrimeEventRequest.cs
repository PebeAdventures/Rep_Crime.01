using Rep_Crime._01_Crime.API.Models;

namespace Rep_Crime._01_Crime.API.Factories
{
    public class CrimeEventRequest
    {
        public CrimeEventRequest(EventType eventType, string description, string placeOfEvent, string reportingPersonEmail, EventStatus eventStatus)
        {
            EventType = eventType;
            Description = description;
            PlaceOfEvent = placeOfEvent;
            ReportingPersonEmail = reportingPersonEmail;
            EventStatus = eventStatus;
        }

        public EventType EventType { get; }
        public string Description { get; }
        public string PlaceOfEvent { get; }
        public string ReportingPersonEmail { get; }
        public EventStatus EventStatus { get; }



    }
}
