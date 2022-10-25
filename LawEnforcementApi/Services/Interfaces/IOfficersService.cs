using LawEnforcementApi.DTOs;

namespace LawEnforcementApi.Services.Interfaces;

public interface IOfficersService
{
    Task<IEnumerable<OfficerReadDto>> GetAllAsync();
    Task<OfficerReadDto> GetSingleAsync(string callSign);
    Task<OfficerReadDto> CreateOfficerAsync(OfficerCreateDto officerCreateDto);
    Task<CallSignDto> AssignCaseToOfficerAsync(string crimeEventId);
}
