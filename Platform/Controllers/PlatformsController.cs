using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Platform.SyncDataServices.Http;
using PlatformReadDtoService.Dtos;
using PlatformService.Data;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _platformRepository;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;

    public PlatformsController(IMapper mapper,
                               IPlatformRepository platformRepository,
                               ICommandDataClient commandDataClient)
    {
        _platformRepository = platformRepository;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatfroms()
    {
        Console.WriteLine("Getting platforms");

        var platformItems = _platformRepository.GetAllPlatforms();
        var mappedItems = _mapper.Map<IEnumerable<PlatformReadDto>>(platformItems);

        return Ok(mappedItems);
    }

    [HttpGet("{platformId}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int platformId)
    {
        var platformItem = _platformRepository.GetPlatformById(platformId);
        if (platformItem == null)
        {
            return NotFound();
        }
        var mappedItem = _mapper.Map<PlatformReadDto>(platformItem);

        return Ok(mappedItem);
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platform)
    {
        var platformModel = _mapper.Map<Models.Platform>(platform);
        _platformRepository.CreatePlatform(platformModel);
        _platformRepository.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await _commandDataClient.SendPlatfromToCommand(platformReadDto);
        }
        catch (Exception exception)
        {
            System.Console.WriteLine($"---> Could not send synchronously: {exception.Message}");
        }
        //return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformModel);
        return Ok(platformReadDto);
    }
}