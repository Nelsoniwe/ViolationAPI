using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
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
    [Route("ById/{id}")]
    public async Task<ActionResult<IEnumerable<VideoModel>>> GetVideoById(int id)
    {
        return Ok(await _videoService.GetVideoById(id));
    }

    /// <summary>
    /// Get video by application id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ByApplicationId/{id}")]
    public async Task<ActionResult<IEnumerable<VideoModel>>> GetVideoByApplicationId(int id)
    {
        var application = await _applicationService.GetApplicationById(id);
        if (application.PhotoId == null)
        {
            return BadRequest("Application doesn't contain video");
        }
        return Ok(await _videoService.GetVideoById((int)application.PhotoId));
    }

    /// <summary>
    /// Create video
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<string>> CreateVideo([FromForm] IFormFile video)
    {
        var videoDto = new VideoDTO();

        if (video != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await video.CopyToAsync(memoryStream);
                videoDto.data = memoryStream.ToArray();
            }
        }

        videoDto.FileName = video.FileName;
        await _videoService.AddVideo(videoDto);

        var resultVideo = await _videoService.GetVideoByHash(videoDto.Hash);
        return Ok(resultVideo.Id);
    }
}