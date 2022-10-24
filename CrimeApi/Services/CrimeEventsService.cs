using AutoMapper;
using CrimeApi.DAL.Interfaces;
using CrimeApi.DTOs;
using CrimeApi.Entities;
using CrimeApi.Services.Interfaces;

namespace CrimeApi.Services;

public class CrimeEventsService : ICrimeEventsService
{
    private readonly ICrimeEventsDAO _crimeEventsDAO;
    private readonly IMapper _mapper;

    public CrimeEventsService(ICrimeEventsDAO crimeEventsDAO, IMapper mapper)
    {
        _crimeEventsDAO = crimeEventsDAO;
        _mapper = mapper;
    }
    public async Task<CrimeEventReadDto> CreateCrimeEventAsync(CrimeEventCreateDto eventDto)
    {
        var eventToCreate = _mapper.Map<CrimeEvent>(eventDto);
        await _crimeEventsDAO.CreateCrimeEventAsync(eventToCreate);
        return _mapper.Map<CrimeEventReadDto>(eventToCreate);
    }

    public async Task<IEnumerable<CrimeEventReadDto>> GetAllCrimeEventsAsync()
    {
        var allEvents = await _crimeEventsDAO.GetAllAsync();
        return _mapper.Map<IEnumerable<CrimeEventReadDto>>(allEvents);
    }
}
