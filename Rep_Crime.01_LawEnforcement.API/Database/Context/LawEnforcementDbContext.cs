using Microsoft.EntityFrameworkCore;
using Rep_Crime._01_LawEnforcement.API.Models;

namespace Rep_Crime._01_LawEnforcement.API.Database.Context
{
    public class LawEnforcementDbContext : DbContext
    {

        public DbSet<LawEnforcement> lawEnforcements { get; set; }

        public LawEnforcementDbContext(DbContextOptions<LawEnforcementDbContext> options) : base(options)
        { }
    }
}
