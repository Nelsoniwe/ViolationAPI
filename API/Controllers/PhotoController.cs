using System.Security.Claims;
using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using BLL.Utility;
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
    [Route("ById/{id}")]
    public async Task<ActionResult<IEnumerable<PhotoModel>>> GetPhotoById(int id)
    {
        return Ok(await _photoService.GetPhotoById(id));
    }

    /// <summary>
    /// Get photo by application id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ByApplicationId/{id}")]
    public async Task<ActionResult<IEnumerable<PhotoModel>>> GetPhotoByApplicationId(int id)
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
    public async Task<ActionResult<PhotoDTO>> CreatePhoto([FromForm] IFormFile photo)
    {
        var photoDto = new PhotoDTO();

        if (photo != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await photo.CopyToAsync(memoryStream);
                photoDto.data = memoryStream.ToArray();
            }
        }

        photoDto.FileName = photo.FileName;
        await _photoService.AddPhoto(photoDto);

        var resultPhoto = await _photoService.GetPhotoByHash(photoDto.Hash);
        return Ok(resultPhoto.Id);
    }
}