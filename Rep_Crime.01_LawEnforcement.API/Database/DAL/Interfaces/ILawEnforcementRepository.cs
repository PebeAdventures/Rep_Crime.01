using Rep_Crime._01_LawEnforcement.API.Models;

namespace Rep_Crime._01_LawEnforcement.API.Database.DAL.Interfaces
{
    public interface ILawEnforcementRepository
    {

        Task<List<LawEnforcement>> GetAllLawEnforcementsAsync();
        Task<LawEnforcement> GetLawEnforcementByIdAsync(string publicId);
        Task<List<AssignedCrimeEvent>> GetAllAssignedCrimeEventFromChosedLawEnforcement(string publicId);
        Task AddNewLawEnforcement(LawEnforcement lawEnforcement);
        Task AddNewAssignedCrimeTolawEnforcement(AssignedCrimeEvent assignedCrime, string publicId);
        Task DeleteAssignedCrimeFromLawEnforcement(AssignedCrimeEvent assignedCrime, string publicId);
        Task DeletelawEnforcement(string publicId);
        Task AddNewAssignedCrimeToMostAccessiblelawEnforcement(AssignedCrimeEvent assignedCrime);

    }
}