using System.ComponentModel.DataAnnotations;

namespace LawEnforcementApi.DTOs;

public record AssignOfficerDto
{
    [MaxLength(50)]
    public string CrimeEventId { get; init; } = null!;
}

