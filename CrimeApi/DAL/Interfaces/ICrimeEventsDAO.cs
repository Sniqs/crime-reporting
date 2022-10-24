using CrimeApi.Entities;

namespace CrimeApi.DAL.Interfaces;

public interface ICrimeEventsDAO
{
    Task<IEnumerable<CrimeEvent>> GetAllAsync();
    Task CreateCrimeEventAsync(CrimeEvent crimeEvent);
}
