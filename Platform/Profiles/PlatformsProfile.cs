using AutoMapper;
using PlatformReadDtoService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        // Source ----> Target
        CreateMap<Models.Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Models.Platform>();
    }
}
