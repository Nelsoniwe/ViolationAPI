using System.Security.Claims;
using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for work with photos
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PhotoController : ControllerBase
{
    private readonly IPhotoService _photoService;
    private readonly IApplicationService _applicationService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public PhotoController(IMapper mapper, IUserService userService, IPhotoService photoService, IApplicationService applicationService)
    {
        _mapper = mapper;
        _userService = userService;
        _photoService = photoService;
        _applicationService = applicationService;
    }

    /// <summary>
    /// Get photo by id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("GetById")]
    public async Task<ActionResult<IEnumerable<VehicleColorDTO>>> GetPhotoById(int id)
    {
        return Ok(await _photoService.GetPhotoById(id));
    }

    /// <summary>
    /// Get photo by application id
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
        return Ok(await _photoService.GetPhotoById((int)application.PhotoId));
    }

    /// <summary>
    /// Create photo
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> CreatePhoto([FromBody] PhotoModel photo)
    {
        var newPhotoId = await _photoService.AddPhoto(_mapper.Map<PhotoDTO>(photo));
        return Ok();
    }
}