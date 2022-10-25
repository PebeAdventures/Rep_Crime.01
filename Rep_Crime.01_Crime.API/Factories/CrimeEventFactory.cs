using Rep_Crime._01_Crime.API.Factories.Interface;
using Rep_Crime._01_Crime.API.Models;

namespace Rep_Crime._01_Crime.API.Factories
{
    public class CrimeEventFactory : ICrimeEventFactory
    {

        public CrimeEvent Create(CrimeEventRequest crimeEventRequest)
        {
            CrimeEvent crimeEvent = CreateCrimeEventFromRequest(crimeEventRequest);
            return crimeEvent;
        }

        private CrimeEvent CreateCrimeEventFromRequest(CrimeEventRequest crimeEventRequest)
        {
            CrimeEvent crimeEvent = new CrimeEvent();
            crimeEvent.EventType = crimeEventRequest.EventType;
            crimeEvent.Description = crimeEventRequest.Description;
            crimeEvent.PlaceOfEvent = crimeEventRequest.PlaceOfEvent;
            crimeEvent.ReportingPersonEmail = crimeEventRequest.ReportingPersonEmail;
            crimeEvent.DateTime = DateTime.Now;
            crimeEvent.PublicIdentifier = new string(crimeEvent.EventType.ToString().Take(3).ToArray()) + crimeEvent.DateTime.Ticks;
            return crimeEvent;

        }
    }
}
