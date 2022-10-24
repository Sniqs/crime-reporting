using AutoMapper;
using LawEnforcementApi.DTOs;
using LawEnforcementApi.Entities;

namespace LawEnforcementApi.Profiles;

public class OfficerProfile : Profile
{
    public OfficerProfile()
    {
        CreateMap<Officer, OfficerReadDto>()
            .ForMember(o => o.Rank, opt => opt.MapFrom(r => r.OfficerRank.Name))
            .ForMember(o => o.CrimeEvents, opt => opt.MapFrom(o => o.CrimeEvents.Select(e => e.CrimeEventId)));
    }
}
