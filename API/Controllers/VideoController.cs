using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for work with videos
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IApplicationService _applicationService;

    public VideoController(IMapper mapper, IUserService userService, IVideoService videoService, IApplicationService applicationService)
    {
        _mapper = mapper;
        _userService = userService;
        _videoService = videoService;
        _applicationService = applicationService;
    }

    /// <summary>
    /// Get video by id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("GetById")]
    public async Task<ActionResult<IEnumerable<VehicleColorDTO>>> GetPhotoById(int id)
    {
        return Ok(await _videoService.GetVideoById(id));
    }

    /// <summary>
    /// Get video by application id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("GetByApplicationId")]
    public async Task<ActionResult<IEnumerable<VehicleColorDTO>>> GetPhotoByApplicationId(int id)
    {
        var application = await _applicationService.GetApplicationById(id);
        if (application.PhotoId == null)
        {
            return BadRequest("Application doesn't contain photo");
        }
        return Ok(await _videoService.GetVideoById((int)application.PhotoId));
    }

    /// <summary>
    /// Create video
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> CreatePhoto([FromBody] VideoModel photo)
    {
        var newPhotoId = await _videoService.AddVideo(_mapper.Map<VideoDTO>(photo));
        return Ok();
    }
}