using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformReadDtoService.Dtos;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _platformRepository;
    private readonly IMapper _mapper;
    public PlatformsController(IMapper mapper, IPlatformRepository platformRepository)
    {
        _platformRepository = platformRepository;
        _mapper = mapper;
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
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platform)
    {
        var platformModel = _mapper.Map<Platform>(platform);
        _platformRepository.CreatePlatform(platformModel);
        _platformRepository.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        // return CreatedAtRoute("GetPlatformById", new { Id = platformReadDto.Id }, platformReadDto);
        return Ok(platformReadDto);
    }
}