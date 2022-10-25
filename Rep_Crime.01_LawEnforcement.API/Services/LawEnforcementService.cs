using Rep_Crime._01_LawEnforcement.API.Database.DAL.Interfaces;
using Rep_Crime._01_LawEnforcement.API.Models;
using Rep_Crime._01_LawEnforcement.API.Services.Interface;

namespace Rep_Crime._01_LawEnforcement.API.Services
{
    public class LawEnforcementService : ILawEnforcementService
    {

        private readonly IUnitOfWork _unitOfWork;
        public LawEnforcementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task AddLawEnforcementToBase(LawEnforcement lawEnforcement)
        {
            await _unitOfWork.LawEnforcementRepository.AddNewLawEnforcement(lawEnforcement);
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
        public async Task UpdateAssignedCrimeStatus(int newStatusNumber, AssignedCrimeEvent assignedCrimeEvent)
        {
            //wysłanie do CrimeEvent API informacji o zmianie statusu konkretnego CrimeEventu
        }


    }
}
