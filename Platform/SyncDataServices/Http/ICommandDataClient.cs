using PlatformReadDtoService.Dtos;

namespace Platform.SyncDataServices.Http;

public interface ICommandDataClient
{
    Task SendPlatfromToCommand(PlatformReadDto platform);
}
