using System.Text;
using System.Text.Json;
using PlatformReadDtoService.Dtos;

namespace Platform.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;

    public HttpCommandDataClient(HttpClient httpClient,)
    {
        _httpClient = httpClient;
    }

    public async Task SendPlatfromToCommand(PlatformReadDto platform)
    {
        var httpContent = new StringContent(
                    JsonSerializer.Serialize(platform),
                    Encoding.UTF8,
                    "application/Json");

        var response = await _httpClient.PostAsync("http://localhost:5146/api/command/Platforms/", httpContent);

        if (response.IsSuccessStatusCode)
        {
            System.Console.WriteLine("---> Sync POST to CommandService was OK!");
        }
        else
        {
            System.Console.WriteLine("---> Sync POST to CommandService was NOT OK!");
        }
    }
}
