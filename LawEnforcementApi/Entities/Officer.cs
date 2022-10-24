namespace LawEnforcementApi.Entities;

public class Officer
{
    public Guid Id { get; set; }
    public string CallSign { get; set; } = null!;
    public Rank OfficerRank { get; set; } = null!;
    public ICollection<CrimeEvent> CrimeEvents { get; set; } = null!;
}
