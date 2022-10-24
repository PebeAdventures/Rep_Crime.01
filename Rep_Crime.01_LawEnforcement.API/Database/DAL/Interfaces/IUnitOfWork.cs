namespace Rep_Crime._01_LawEnforcement.API.Database.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ILawEnforcementRepository LawEnforcementRepository { get; }
    }
}