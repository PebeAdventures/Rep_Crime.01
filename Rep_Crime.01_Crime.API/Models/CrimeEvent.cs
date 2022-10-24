using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rep_Crime._01_Crime.API.Models
{
    public class CrimeEvent
    {
        [BsonId]
        public string Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateTime { get; set; }
        public EventType EventType { get; set; }
        public string Description { get; set; }
        public string PlaceOfEvent { get; set; }
        public string ReportingPersonEmail { get; set; }
        public EventStatus EventStatus { get; set; }
        public string AssigneLawEnforcementID { get; set; }


    }
}
