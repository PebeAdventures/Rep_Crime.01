using Rep_Crime._01_LawEnforcement.API.Models;

namespace Rep_Crime._01_LawEnforcement.API.Factories
{
    public class LawEnforcementRequest
    {
        public LawEnforcementRank LawEnforcementRank { get; }
        public List<AssignedCrimeEvent> AssignedCrimeEvents { get; }

        public LawEnforcementRequest(List<AssignedCrimeEvent> assignedCrimeEvents, LawEnforcementRank lawEnforcementRank)
        {
            AssignedCrimeEvents = assignedCrimeEvents;
            LawEnforcementRank = lawEnforcementRank;
        }
    }
}
