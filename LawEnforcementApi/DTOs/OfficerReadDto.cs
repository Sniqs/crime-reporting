namespace LawEnforcementApi.DTOs;

public record OfficerReadDto
{
    public string CallSign { get; init; } = null!;
    public string Rank { get; init; } = null!;
    public ICollection<string> CrimeEvents { get; init; } = null!;
}
