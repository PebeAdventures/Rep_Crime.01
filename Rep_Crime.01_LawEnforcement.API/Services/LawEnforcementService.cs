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


        public async Task AddNewAssignedCrimeToChosedLawEnforcement(AssignedCrimeEvent assignedCrimeEvent)
        {
            await _unitOfWork.LawEnforcementRepository.AddNewAssignedCrimeToMostAccessiblelawEnforcement(assignedCrimeEvent);
        }
        public async Task AddNewAssignedCrimeToChosedLawEnforcement(AssignedCrimeEvent assignedCrimeEvent, string publicId)
        {
            await _unitOfWork.LawEnforcementRepository.AddNewAssignedCrimeTolawEnforcement(assignedCrimeEvent, publicId);
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
