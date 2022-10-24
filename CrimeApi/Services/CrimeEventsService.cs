using AutoMapper;
using CrimeApi.DAL.Interfaces;
using CrimeApi.DTOs;
using CrimeApi.Entities;
using CrimeApi.Exceptions;
using CrimeApi.Services.Interfaces;
using MongoDB.Bson;

namespace CrimeApi.Services;

public class CrimeEventsService : ICrimeEventsService
{
    private readonly ICrimeEventsDAO _crimeEventsDAO;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;

    public CrimeEventsService(ICrimeEventsDAO crimeEventsDAO, IMapper mapper, IHttpClientFactory clientFactory, IConfiguration configuration)
    {
        _crimeEventsDAO = crimeEventsDAO;
        _mapper = mapper;
        _clientFactory = clientFactory;
        _configuration = configuration;
    }
    public async Task<CrimeEventReadDto> CreateCrimeEventAsync(CrimeEventCreateDto eventDto)
    {
        var eventToCreate = _mapper.Map<CrimeEvent>(eventDto);
        await _crimeEventsDAO.CreateCrimeEventAsync(eventToCreate);
        await AssignOfficerAsync(eventToCreate);
        return _mapper.Map<CrimeEventReadDto>(eventToCreate);
    }

    private async Task AssignOfficerAsync(CrimeEvent eventToCreate)
    {
        var client = _clientFactory.CreateClient();
        var callSignDto = await client.GetFromJsonAsync<CallSignDto>($"{_configuration["LawEnforcementApiBaseUrl"]}/api/Officers/assign/{eventToCreate.Id}");
        
        if (callSignDto == null)
        {
            throw new NoOfficerAvailableException("No officers available. Try again later.");
        }

        eventToCreate.AssignedOfficerCallSign = callSignDto.CallSign;
        await _crimeEventsDAO.UpdateCrimeEventAsync(eventToCreate);
    }

    public async Task<IEnumerable<CrimeEventReadDto>> GetAllCrimeEventsAsync()
    {
        var allEvents = await _crimeEventsDAO.GetAllAsync();
        return _mapper.Map<IEnumerable<CrimeEventReadDto>>(allEvents);
    }

    public async Task<CrimeEventReadDto> GetSingleCrimeEventAsync(string id)
    {
        var crimeEvent = await _crimeEventsDAO.GetSingleAsync(id);
        return _mapper.Map<CrimeEventReadDto>(crimeEvent);
    }
}
