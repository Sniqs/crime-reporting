using AutoMapper;
using LawEnforcementApi.Contexts;
using LawEnforcementApi.DTOs;
using LawEnforcementApi.Entities;
using LawEnforcementApi.Exceptions;
using LawEnforcementApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Services;

public class OfficersService : IOfficersService
{
    private readonly LawEnforcementContext _dbcontext;
    private readonly IMapper _mapper;
    private readonly ILogger<OfficersService> _logger;

    public OfficersService(LawEnforcementContext dbcontext, IMapper mapper, ILogger<OfficersService> logger)
    {
        _dbcontext = dbcontext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CallSignDto> AssignCaseToOfficerAsync(string crimeEventId)
    {
        _logger.LogDebug("Attempting to get a random officer");
        var officer = await GetRandomOfficerAsync();
        var crimeEvent = new CrimeEvent { CrimeEventId = crimeEventId, OfficerId = officer.Id };
        _dbcontext.CrimeEvents.Add(crimeEvent);

        _logger.LogDebug("Attempting to save a new crime event");
        await _dbcontext.SaveChangesAsync();
        return _mapper.Map<CallSignDto>(officer);
    }
    public async Task<IEnumerable<OfficerReadDto>> GetAllAsync()
    {
        _logger.LogDebug("Attempting to get all officers from persistence");
        var officers = await _dbcontext.Officers
            .Include(o => o.OfficerRank)
            .Include(o => o.CrimeEvents)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<OfficerReadDto>>(officers);
    }

    public async Task<OfficerReadDto> GetSingleAsync(string callSign)
    {
        _logger.LogDebug("Attempting to get a single officer from persistence");
        var officer = await _dbcontext.Officers
            .Where(o => o.CallSign == callSign)
            .Include(o => o.OfficerRank)
            .Include(o => o.CrimeEvents)
            .FirstOrDefaultAsync();

        if (officer is null)
        {
            _logger.LogDebug("Persistence returned a null officer");
            throw new ResourceNotFoundException($"Officer with call sign '{callSign}' doesn't exist.");
        }

        return _mapper.Map<OfficerReadDto>(officer);
    }

    public async Task<OfficerReadDto> CreateOfficerAsync(OfficerCreateDto officerCreateDto)
    {
        await CheckIfCallSignTakenAsync(officerCreateDto.CallSign);
        var officer = _mapper.Map<Officer>(officerCreateDto);
        var rank = await GetRank(officerCreateDto.Rank);
        officer.OfficerRank = rank;

        _logger.LogDebug("Attempting to persist an officer");
        _dbcontext.Officers.Add(officer);
        await _dbcontext.SaveChangesAsync();

        return _mapper.Map<OfficerReadDto>(officer);
    }

    private async Task CheckIfCallSignTakenAsync(string callSign)
    {
        var officer = await _dbcontext.Officers.FirstOrDefaultAsync(o => o.CallSign == callSign);
        if (officer is not null)
            throw new DuplicateException("An officer with the provided call sign already exists.");
    }

    private async Task<Rank> GetRank(string providedRank)
    {
        var rank = await _dbcontext.Ranks.FirstOrDefaultAsync(r => r.Name == providedRank);
        if (rank is null)
        {
            _logger.LogDebug("Persistence returned a null rank");
            throw new ResourceNotFoundException($"Rank '{providedRank}' doesn't exist.");
        }
        return rank;
    }

    private async Task<Officer> GetRandomOfficerAsync()
    {
        _logger.LogDebug("Attempting to get the number of officers in persistence");
        var numberOfOfficers = await _dbcontext.Officers.CountAsync();

        if (numberOfOfficers == 0)
            throw new ResourceNotFoundException($"No officers available.");

        var randomOfficer = RandomNumberGenerator.GetRandomFromRange(0, numberOfOfficers);
        var officer = await _dbcontext.Officers.Skip(randomOfficer).Take(1).FirstOrDefaultAsync();

        if (officer is null)
        {
            _logger.LogDebug("Persistence returned a null officer");
            throw new ResourceNotFoundException($"No officers available.");
        }
        return officer;
    }
}
