using CrimeApi.Entities;

namespace CrimeApi.DAL.Interfaces;

public interface ICrimeEventsDAO
{
    Task<IEnumerable<CrimeEvent>> GetAllAsync();
    Task<CrimeEvent> GetSingleAsync(string id);
    Task CreateCrimeEventAsync(CrimeEvent crimeEvent);
    Task UpdateCrimeEventAsync(CrimeEvent crimeEvent);
}
