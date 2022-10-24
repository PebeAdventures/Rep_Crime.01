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


        public async Task<List<LawEnforcement>> GetAllLawEnforcements()
        {
            var lawEnforcementsList = await lawEnforcementDbContext.lawEnforcements.Select(x => x).ToListAsync();
            return lawEnforcementsList;
        }

    }


}
