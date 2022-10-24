using AutoMapper;
using CrimeApi.DTOs;
using CrimeApi.Entities;

namespace CrimeApi.Profiles;

public class CrimeEventProfile : Profile
{
	public CrimeEventProfile()
	{
		CreateMap<CrimeEvent, CrimeEventReadDto>();
		CreateMap<CrimeEventCreateDto, CrimeEvent>();
	}
}
