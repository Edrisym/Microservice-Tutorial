using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using CommandsService.Models;
namespace CommandService.Profiles;

public class CommandsProfiles : Profile
{
    public CommandsProfiles()
    {
        // source -> target
        CreateMap<Platform, PlatfromReadDto>();
        CreateMap<CommandCreateDto, Command>();
        CreateMap<Command, CommandReadDto>();
    }

}
