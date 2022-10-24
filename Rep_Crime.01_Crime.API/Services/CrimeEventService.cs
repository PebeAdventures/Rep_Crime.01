using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Rep_Crime._01_Crime.API.Models;
using Rep_Crime._01_Crime.API.Services.Interface;

namespace Rep_Crime._01_Crime.API.Services
{
    public class CrimeEventService : ICrimeEventService
    {
        private readonly IMongoCollection<CrimeEvent> _crimeEventsCollection;
        public CrimeEventService(IOptions<CrimeEventsDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _crimeEventsCollection = mongoDatabase.GetCollection<CrimeEvent>(options.Value.CrimeCollectionName);
        }

        public async Task<List<CrimeEvent?>> GetAllEvents() =>
       await _crimeEventsCollection.Find(_ => true).ToListAsync();


        public async Task<CrimeEvent?> GetCrimeEventById(string id) =>
        await _crimeEventsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();


        public async Task<List<CrimeEvent?>> GetAllEventsByType(EventType eventType) =>
        await _crimeEventsCollection.Find(x => x.EventType == eventType).ToListAsync();


        public async Task CreateEventAsync(CrimeEvent newCrimeEvent) =>
        await _crimeEventsCollection.InsertOneAsync(newCrimeEvent);


        public async Task UpdateCrimeEventStatus(string id, CrimeEvent updatedCrimeEvent)

        {
            await _crimeEventsCollection.ReplaceOneAsync(x => x.Id == id, updatedCrimeEvent);
        }


        public async Task RemoveAsync(string id) =>
       await _crimeEventsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
