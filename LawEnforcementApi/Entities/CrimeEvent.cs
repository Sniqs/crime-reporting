namespace LawEnforcementApi.Entities;

public class CrimeEvent
{
    public Guid Id { get; set; }
    public string CrimeEventId { get; set; } = null!;
    public Guid OfficerId { get; set; }
}
