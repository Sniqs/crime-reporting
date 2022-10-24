namespace LawEnforcementApi.DTOs;

public record OfficerReadDto
{
    public string CallSign { get; set; } = null!;
    public string Rank { get; set; } = null!;
    public ICollection<string> CrimeEvents { get; set; } = null!;
}
