using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Rep_Crime._01_Crime.API.Models
{
    public class CrimeEvent
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateTime { get; set; }
        public EventType EventType { get; set; }
        public string Description { get; set; }
        public string PlaceOfEvent { get; set; }
        public string ReportingPersonEmail { get; set; }
        public EventStatus EventStatus { get; set; }
        public string AssigneLawEnforcementID { get; set; }
        public string PublicIdentifier { get; set; }


    }
}
