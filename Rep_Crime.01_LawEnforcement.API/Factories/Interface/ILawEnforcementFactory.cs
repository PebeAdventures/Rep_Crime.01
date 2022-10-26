using Rep_Crime._01_LawEnforcement.API.Models;

namespace Rep_Crime._01_LawEnforcement.API.Factories.Interface
{
    public interface ILawEnforcementFactory
    {
        LawEnforcement Create(LawEnforcementRequest lawEnforcementRequest);
    }
}