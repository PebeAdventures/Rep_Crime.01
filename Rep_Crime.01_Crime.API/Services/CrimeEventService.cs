using Commons.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Rep_Crime._01_Crime.API.Factories;
using Rep_Crime._01_Crime.API.Models;
using Rep_Crime._01_Crime.API.Services.Interface;
using System.Text;

namespace Rep_Crime._01_Crime.API.Services
{
    public class CrimeEventService : ICrimeEventService
    {
        private readonly IMongoCollection<CrimeEvent> _crimeEventsCollection;
        private readonly HttpClient _httpClient;
        public CrimeEventService(IOptions<CrimeEventsDatabaseSettings> options, HttpClient httpClient)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _crimeEventsCollection = mongoDatabase.GetCollection<CrimeEvent>(options.Value.CrimeCollectionName);
            _httpClient = httpClient;
        }

        public async Task<List<CrimeEvent?>> GetAllEvents() =>
       await _crimeEventsCollection.Find(_ => true).ToListAsync();


        public async Task<CrimeEvent?> GetCrimeEventById(string id) =>
        await _crimeEventsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();


        public async Task<List<CrimeEvent?>> GetAllEventsByType(EventType eventType) =>
        await _crimeEventsCollection.Find(x => x.EventType == eventType).ToListAsync();


        public async Task CreateEventAsync(NewCrimeEventDTO newCrimeEventDTO)
        {
            //EventType walidacja i przepisanie na etapie controlera
            CrimeEventRequest crimeEventRequest = new CrimeEventRequest(
                EventType.ASSAULT,
                newCrimeEventDTO.Description,
                newCrimeEventDTO.PlaceOfEvent,
                newCrimeEventDTO.ReportingPersonEmail,
                EventStatus.WAITING);

            CrimeEvent newCrimeEvent = new CrimeEventFactory().Create(crimeEventRequest);
            string lawEnforcementId = await ProxyTo("https://localhost:7113/addNewAssignedCrimeToMostAccessibleLawEnforcement/", newCrimeEvent.PublicIdentifier);
            newCrimeEvent.AssigneLawEnforcementID = lawEnforcementId;
            //wysłanie info do LawEnf z dodaniem publicId do ich bazy i przypisaniem przypisanego lawEnf Id tu

            await _crimeEventsCollection.InsertOneAsync(newCrimeEvent);
        }

        private async Task<string> ProxyTo(string url, string value)
        {
            var content = new StringContent(value, Encoding.UTF8, "application/json");
            var respond = await _httpClient.PostAsync(url, content);
            var result = await respond.Content.ReadAsStringAsync();
            return result;

        }

        public async Task UpdateCrimeEventStatus(string id, CrimeEvent updatedCrimeEvent)

        {
            await _crimeEventsCollection.ReplaceOneAsync(x => x.Id == id, updatedCrimeEvent);
        }


        public async Task RemoveAsync(string id) =>
       await _crimeEventsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
