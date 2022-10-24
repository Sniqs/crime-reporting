using CrimeApi.DTOs;

namespace CrimeApi.Services.Interfaces;

public interface ICrimeEventsService
{
    Task<IEnumerable<CrimeEventReadDto>> GetAllCrimeEventsAsync();
    Task<CrimeEventReadDto> CreateCrimeEventAsync(CrimeEventCreateDto eventDto);
}
