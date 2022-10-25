﻿using Rep_Crime._01_LawEnforcement.API.Models;

namespace Rep_Crime._01_LawEnforcement.API.Services.Interface
{
    public interface ILawEnforcementService
    {
        Task AddLawEnforcementToBase(LawEnforcement lawEnforcement);
        Task AddNewAssignedCrimeToChosedLawEnforcement(AssignedCrimeEvent assignedCrimeEvent, string publicId);
        Task<string> AddNewAssignedCrimeToMostAccessibleLawEnforcement(AssignedCrimeEvent assignedCrimeEvent);
        Task DeleteAssignedCrimeFromLawEnforcement(AssignedCrimeEvent assignedCrimeEvent, string publicId);
        Task DeleteLawEnforcement(string publicId);
        Task<List<AssignedCrimeEvent>> GetAllAssignedCrimeFromChosedLawEnforcement(string publicId);
        Task<List<LawEnforcement>> GetAllLawEnforcement();
        Task<LawEnforcement> GetLawEnforcementById(string publicId);
        Task UpdateAssignedCrimeStatus(int newStatusNumber, AssignedCrimeEvent assignedCrimeEvent);
    }
}