using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rep_Crime._01_LawEnforcement.API.Database.Context;
using Rep_Crime._01_LawEnforcement.API.Database.DAL.Interfaces;
using Rep_Crime._01_LawEnforcement.API.Models;

namespace Rep_Crime._01_LawEnforcement.API.Database.DAL
{
    public class LawEnforcementRepository : ILawEnforcementRepository
    {
        private readonly LawEnforcementDbContext lawEnforcementDbContext;

        public LawEnforcementRepository(LawEnforcementDbContext lawEnforcementDbContext)
        {
            this.lawEnforcementDbContext = lawEnforcementDbContext;
        }


        public async Task<List<LawEnforcement>> GetAllLawEnforcementsAsync()
        {
            var lawEnforcementsList = await lawEnforcementDbContext.lawEnforcements.Select(x => x).ToListAsync();
            return lawEnforcementsList;
        }


        public async Task<LawEnforcement> GetLawEnforcementByIdAsync(string publicId)
        {
            LawEnforcement lawEnforcement = await lawEnforcementDbContext.lawEnforcements.Where(x => x.PublicLawEnforcementId.Equals(publicId)).FirstOrDefaultAsync();
            return lawEnforcement;
        }


        public async Task AddNewLawEnforcement(LawEnforcement lawEnforcement)
        {
            await lawEnforcementDbContext.lawEnforcements.AddAsync(lawEnforcement);
            await lawEnforcementDbContext.SaveChangesAsync();

        }
        public async Task DeletelawEnforcement(string publicId)
        {
            var lawEnforcement = await GetLawEnforcementByIdAsync(publicId);
            lawEnforcementDbContext.lawEnforcements.Remove(lawEnforcement);
            await lawEnforcementDbContext.SaveChangesAsync();
        }

        public async Task<List<AssignedCrimeEvent>> GetAllAssignedCrimeEventFromChosedLawEnforcement(string publicId)
        {
            var assignedCrimeEvents = await lawEnforcementDbContext.lawEnforcements.Where(x => x.PublicLawEnforcementId.Equals(publicId)).SelectMany(x => x.AssignedCrimeEvents).ToListAsync();

            return assignedCrimeEvents;
        }

        public async Task AddNewAssignedCrimeToMostAccessiblelawEnforcement(AssignedCrimeEvent assignedCrime)
        {
            var lawEnforcements = await GetAllLawEnforcementsAsync();

            LawEnforcement mostAccesibleLawEnforcement = GetMostAccesibleLawEnforcement(lawEnforcements);

            mostAccesibleLawEnforcement.AssignedCrimeEvents.Add(assignedCrime);
            await lawEnforcementDbContext.SaveChangesAsync();
        }

        private LawEnforcement GetMostAccesibleLawEnforcement(List<LawEnforcement> lawEnforcements)
        {
            LawEnforcement mostAccesibleLawEnforcement = new LawEnforcement();
            LawEnforcement currentLaw = new LawEnforcement();
            int previousControlNumber = 0;
            foreach (LawEnforcement law in lawEnforcements)
            {


                int controlnumber = law.AssignedCrimeEvents.Count();
                if (controlnumber == 0)
                {
                    mostAccesibleLawEnforcement = law;
                    return mostAccesibleLawEnforcement;
                }
                if (controlnumber < previousControlNumber || previousControlNumber == 0)
                {
                    previousControlNumber = controlnumber;
                    currentLaw = law;
                }

            }

            return currentLaw;
        }

        public async Task AddNewAssignedCrimeTolawEnforcement(AssignedCrimeEvent assignedCrime, string publicId)
        {
            LawEnforcement lawEnforcement = await GetLawEnforcementByIdAsync(publicId);
            lawEnforcement.AssignedCrimeEvents.Add(assignedCrime);
            await lawEnforcementDbContext.SaveChangesAsync();
        }

        public async Task DeleteAssignedCrimeFromLawEnforcement(AssignedCrimeEvent assignedCrime, string publicId)
        {
            LawEnforcement lawEnforcement = await GetLawEnforcementByIdAsync(publicId);
            lawEnforcement.AssignedCrimeEvents.Remove(assignedCrime);
        }

    }


}
