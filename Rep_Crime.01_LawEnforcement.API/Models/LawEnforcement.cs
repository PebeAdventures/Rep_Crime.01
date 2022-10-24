namespace Rep_Crime._01_LawEnforcement.API.Models
{
    public class LawEnforcement
    {

        public Guid Id { get; set; }
        public string PublicLawEnforcementId { get; set; }

        public LawEnforcementRank Rank { get; set; }

        public List<AssignedCrimeEvents> AssignedCrimeEvents { get; set; }

    }
}
