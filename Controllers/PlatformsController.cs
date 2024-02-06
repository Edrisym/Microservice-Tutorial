using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PlatformReadDtoService.Dtos;
using PlatformService.Data;

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
}