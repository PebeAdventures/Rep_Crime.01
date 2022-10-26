using Commons.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
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
            _httpClient = new HttpClient();
        }

        public async Task<List<CrimeEvent?>> GetAllEvents() =>
       await _crimeEventsCollection.Find(_ => true).ToListAsync();


        public async Task<CrimeEvent?> GetCrimeEventById(string id) =>
        await _crimeEventsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<CrimeEvent?> GetCrimeEventByPublicId(string publicId) =>
      await _crimeEventsCollection.Find(x => x.PublicIdentifier == publicId).FirstOrDefaultAsync();

        public async Task<List<CrimeEvent?>> GetAllEventsByType(EventType eventType) =>
        await _crimeEventsCollection.Find(x => x.EventType == eventType).ToListAsync();


        public async Task CreateEventAsync(CrimeEventRequest crimeEventRequest)
        {
            CrimeEvent newCrimeEvent = new CrimeEventFactory().Create(crimeEventRequest);
            string crimeEventId = newCrimeEvent.PublicIdentifier;
            var json = JsonConvert.SerializeObject(new CrimeEventIdDTO() { CrimeEventId = "crimeEventId", EventId = crimeEventId });
            string lawEnforcementId = await ProxyTo("http://rep_crime.01_lawenforcement.api/addNewAssignedCrimeToMostAccessibleLawEnforcement/", json);
            newCrimeEvent.AssigneLawEnforcementID = lawEnforcementId;
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
            //Powiadomienie osoby zglaszającej o zmianie statusu
        }

        public async Task<CrimeEventDetailsDTO> GetCrimeEventForLawEnforcement(CrimeEventDetailsDTO crimeEventDetailsDTO)
        {
            CrimeEvent crimeEvent = await GetCrimeEventByPublicId(crimeEventDetailsDTO.CrimeEventId);
            crimeEventDetailsDTO.DateTime = crimeEvent.DateTime;
            crimeEventDetailsDTO.EventType = crimeEvent.EventType.ToString();
            crimeEventDetailsDTO.Description = crimeEvent.Description;
            crimeEventDetailsDTO.PlaceOfEvent = crimeEvent.PlaceOfEvent;
            crimeEventDetailsDTO.ReportingPersonalEmail = crimeEvent.ReportingPersonEmail;
            return crimeEventDetailsDTO;


        }



        public async Task RemoveAsync(string id) =>
       await _crimeEventsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
