using AutoMapper;
using CrimeApi.DAL.Interfaces;
using CrimeApi.DTOs;
using CrimeApi.Entities;
using CrimeApi.Exceptions;
using CrimeApi.Services.Interfaces;

namespace CrimeApi.Services;

public class CrimeEventsService : ICrimeEventsService
{
    private readonly ICrimeEventsDAO _crimeEventsDAO;
    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CrimeEventsService> _logger;

    public CrimeEventsService(
        ICrimeEventsDAO crimeEventsDAO,
        IMapper mapper, 
        IHttpClientFactory clientFactory, 
        IConfiguration configuration,
        ILogger<CrimeEventsService> logger)
    {
        _crimeEventsDAO = crimeEventsDAO;
        _mapper = mapper;
        _clientFactory = clientFactory;
        _configuration = configuration;
        _logger = logger;
    }
    public async Task<CrimeEventReadDto> CreateCrimeEventAsync(CrimeEventCreateDto eventDto)
    {
        var eventToCreate = _mapper.Map<CrimeEvent>(eventDto);

        _logger.LogDebug("Attempting to persist a crime event");
        await _crimeEventsDAO.CreateCrimeEventAsync(eventToCreate);

        _logger.LogDebug("Attempting to assign an officer to the crime event");
        await AssignOfficerAsync(eventToCreate);

        return _mapper.Map<CrimeEventReadDto>(eventToCreate);
    }

    private async Task AssignOfficerAsync(CrimeEvent eventToCreate)
    {
        var client = _clientFactory.CreateClient();
        var assignOfficerDto = new AssignOfficerDto(eventToCreate.Id!);

        _logger.LogDebug("Attempting to send POST request to Law Enforcement API");
        var response = await client.PostAsJsonAsync($"{_configuration["LawEnforcementApiBaseUrl"]}/api/Officers/assign", assignOfficerDto);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogDebug("POST request unsuccessful");
            throw new NoOfficerAvailableException("No officers available");
        }

        var callSignDto = await response.Content.ReadFromJsonAsync<CallSignDto>();
        eventToCreate.AssignedOfficerCallSign = callSignDto!.CallSign;

        _logger.LogDebug("Attempting to update the officer of a crime event");
        await _crimeEventsDAO.UpdateCrimeEventAsync(eventToCreate);
    }

    public async Task<IEnumerable<CrimeEventReadDto>> GetAllCrimeEventsAsync()
    {
        _logger.LogDebug("Attempting to read all crime events from persistence");
        var allEvents = await _crimeEventsDAO.GetAllAsync();
        return _mapper.Map<IEnumerable<CrimeEventReadDto>>(allEvents);
    }

    public async Task<CrimeEventReadDto> GetSingleCrimeEventAsync(string id)
    {
        _logger.LogDebug("Attempting to read a single crime event from persistence");
        var crimeEvent = await _crimeEventsDAO.GetSingleAsync(id);
        return _mapper.Map<CrimeEventReadDto>(crimeEvent);
    }
}
