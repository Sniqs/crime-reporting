using AutoMapper;
using LawEnforcementApi.Contexts;
using LawEnforcementApi.DTOs;
using LawEnforcementApi.Exceptions;
using LawEnforcementApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcementApi.Services;

public class OfficersService : IOfficersService
{
    private readonly LawEnforcementContext _dbcontext;
    private readonly IMapper _mapper;

    public OfficersService(LawEnforcementContext dbcontext, IMapper mapper)
    {
        _dbcontext = dbcontext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OfficerReadDto>> GetAllAsync()
    {
        var officers = await _dbcontext.Officers
            .Include(o => o.OfficerRank)
            .Include(o => o.CrimeEvents)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<OfficerReadDto>>(officers);
    }

    public async Task<OfficerReadDto> GetSingleAsync(string callSign)
    {
        var officer = await _dbcontext.Officers
            .Where(o => o.CallSign == callSign)
            .Include(o => o.OfficerRank)
            .Include(o => o.CrimeEvents)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (officer is null)
            throw new ResourceNotFoundException($"Officer with call sign {callSign} doesn't exist.");

        return _mapper.Map<OfficerReadDto>(officer);
    }
}
