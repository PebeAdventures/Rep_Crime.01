using Rep_Crime._01_LawEnforcement.API.Factories.Interface;
using Rep_Crime._01_LawEnforcement.API.Models;

namespace Rep_Crime._01_LawEnforcement.API.Factories
{
    public class LawEnforcementFactory : ILawEnforcementFactory
    {

        public LawEnforcement Create(LawEnforcementRequest lawEnforcementRequest)
        {
            LawEnforcement lawEnforcement = CreateLawEnforcement(lawEnforcementRequest);
            return lawEnforcement;
        }

        private LawEnforcement CreateLawEnforcement(LawEnforcementRequest lawEnforcementRequest)
        {
            LawEnforcement lawEnforcement = new LawEnforcement();
            lawEnforcement.AssignedCrimeEvents = lawEnforcementRequest.AssignedCrimeEvents;
            lawEnforcement.Rank = lawEnforcementRequest.LawEnforcementRank;
            lawEnforcement.PublicLawEnforcementId = new string(lawEnforcement.Rank.ToString().Take(8).ToArray()) + DateTime.Now.Ticks;
            return lawEnforcement;
        }
    }
}
