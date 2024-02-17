using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PlatformReadDtoService.Dtos;

namespace Platform.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendPlatfromToCommand(PlatformReadDto platform)
    {
        var httpContent = new StringContent(
                    JsonSerializer.Serialize(platform),
                    Encoding.UTF8,
                    "application/Json");

        var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

        if (response.IsSuccessStatusCode)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---> Sync POST to CommandService was OK!", Console.ForegroundColor);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---> Sync POST to CommandService was NOT OK!", Console.ForegroundColor);
        }
    }
}
