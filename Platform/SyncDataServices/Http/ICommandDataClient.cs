using PlatformReadDtoService.Dtos;

namespace Platform.SyncDataServices.Http;

interface ICommandDataClient
{
    Task SendPlatfromToCommand(PlatformReadDto platform);
}
