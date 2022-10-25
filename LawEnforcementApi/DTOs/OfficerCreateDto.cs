using System.ComponentModel.DataAnnotations;

namespace LawEnforcementApi.DTOs;

public record OfficerCreateDto
{
    [MaxLength(20)]
    public string CallSign { get; init; } = null!;
    public string Rank { get; init; } = null!;
}
