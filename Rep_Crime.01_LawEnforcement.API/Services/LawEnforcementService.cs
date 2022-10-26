using Commons.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rep_Crime._01_LawEnforcement.API.Database.DAL.Interfaces;
using Rep_Crime._01_LawEnforcement.API.Factories;
using Rep_Crime._01_LawEnforcement.API.Models;
using Rep_Crime._01_LawEnforcement.API.Services.Interface;
using System.Text;

namespace Rep_Crime._01_LawEnforcement.API.Services
{
    public class LawEnforcementService : ILawEnforcementService
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        public LawEnforcementService(IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
        }



        public async Task AddLawEnforcementToBase(LawEnforcementRequest lawEnforcementRequest)
        {
            LawEnforcement LawEnforcement = new LawEnforcementFactory().Create(lawEnforcementRequest);
            await _unitOfWork.LawEnforcementRepository.AddNewLawEnforcement(LawEnforcement);
        }

        public async Task DeleteLawEnforcement(string publicId)
        {
            await _unitOfWork.LawEnforcementRepository.DeletelawEnforcement(publicId);
        }

        public async Task<List<LawEnforcement>> GetAllLawEnforcement()
        {
            return await _unitOfWork.LawEnforcementRepository.GetAllLawEnforcementsAsync();
        }

        public async Task<LawEnforcement> GetLawEnforcementById(string publicId)
        {
            return await _unitOfWork.LawEnforcementRepository.GetLawEnforcementByIdAsync(publicId);
        }
        public async Task<string> AddNewAssignedCrimeToMostAccessibleLawEnforcement(AssignedCrimeEvent assignedCrimeEvent)
        {

            return await _unitOfWork.LawEnforcementRepository.AddNewAssignedCrimeToMostAccessiblelawEnforcement(assignedCrimeEvent);
        }
        public async Task AddNewAssignedCrimeToChosedLawEnforcement(AssignedCrimeEvent assignedCrimeEvent, string publicId)
        {
            await _unitOfWork.LawEnforcementRepository.AddNewAssignedCrimeTolawEnforcement(assignedCrimeEvent, publicId);
        }

        public async Task DeleteAssignedCrimeFromLawEnforcement(AssignedCrimeEvent assignedCrimeEvent, string publicId)
        {
            await _unitOfWork.LawEnforcementRepository.DeleteAssignedCrimeFromLawEnforcement(assignedCrimeEvent, publicId);
        }

        public async Task<List<AssignedCrimeEvent>> GetAllAssignedCrimeFromChosedLawEnforcement(string publicId)
        {
            return await _unitOfWork.LawEnforcementRepository.GetAllAssignedCrimeEventFromChosedLawEnforcement(publicId);
        }
        public async Task<string> UpdateAssignedCrimeStatus(string crimeStatus, AssignedCrimeEvent assignedCrimeEvent)
        {
            CrimeEventChangeStatusDTO crimeEventChangeStatusDTO = new CrimeEventChangeStatusDTO();
            crimeEventChangeStatusDTO.CrimeStatus = crimeStatus;
            crimeEventChangeStatusDTO.PublicCrimeId = assignedCrimeEvent.CrimeEventId;


            var json = JsonConvert.SerializeObject(crimeEventChangeStatusDTO);
            var results = await ProxyTo("http://rep_crime.01_crime.api/updateCrimeEventStatusByLawEnforcement/", json);
            return results;

        }


        public async Task<CrimeEventDetailsDTO> GetCrimeEventDetailsByCrimeEventPublicId(string crimeEventPublicId)
        {

            var crimeEventDetailsDTO = new CrimeEventDetailsDTO();
            crimeEventDetailsDTO.CrimeEventId = crimeEventPublicId;
            crimeEventDetailsDTO.PlaceOfEvent = "";
            crimeEventDetailsDTO.ReportingPersonalEmail = "";
            crimeEventDetailsDTO.EventType = "";
            crimeEventDetailsDTO.DateTime = DateTime.Now;
            crimeEventDetailsDTO.Description = "";
            var json = JsonConvert.SerializeObject(crimeEventDetailsDTO);
            var respond = await ProxyTo("http://rep_crime.01_crime.api/getCrimeEventDetailsForLawEnforcement/", json);
            var filledCrimeEventDetails = JsonConvert.DeserializeObject<CrimeEventDetailsDTO>(respond);
            return filledCrimeEventDetails;

        }
        private async Task<string> ProxyTo(string url, string value)
        {
            var content = new StringContent(value, Encoding.UTF8, "application/json");
            var respond = await _httpClient.PostAsync(url, content);
            var result = await respond.Content.ReadAsStringAsync();
            return result;

        }
    }
}
