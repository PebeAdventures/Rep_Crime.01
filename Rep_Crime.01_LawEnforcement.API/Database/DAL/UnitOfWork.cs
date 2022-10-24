using Rep_Crime._01_LawEnforcement.API.Database.DAL.Interfaces;

namespace Rep_Crime._01_LawEnforcement.API.Database.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILawEnforcementRepository LawEnforcementRepository { get; }

        public UnitOfWork(ILawEnforcementRepository lawEnforcementRepository)
        {
            LawEnforcementRepository = lawEnforcementRepository;
        }
    }
}
