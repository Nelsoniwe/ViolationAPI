using System.Security.Claims;
using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for work with application statuses
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ApplicationStatusController : ControllerBase
{
    private readonly IApplicationStatusService _applicationStatusService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ApplicationStatusController(IMapper mapper, IUserService userService, IApplicationStatusService applicationStatusService)
    {
        _mapper = mapper;
        _userService = userService;
        _applicationStatusService = applicationStatusService;
    }

    /// <summary>
    /// Get all application statuses
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ApplicationStatusDTO>>> GetAllApplicationStatuses()
    {
        return Ok(await _applicationStatusService.GetAllApplicationStatuses());
    }

    /// <summary>
    /// Get application status by name
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("GetByName")]
    public async Task<ActionResult<IEnumerable<ApplicationStatusDTO>>> GetApplicationStatusByName(string name)
    {
        return Ok(await _applicationStatusService.GetApplicationStatusByName(name));
    }

    /// <summary>
    /// Get application status by id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("GetById")]
    public async Task<ActionResult<IEnumerable<ApplicationStatusDTO>>> GetApplicationStatusById(int id)
    {
        return Ok(await _applicationStatusService.GetApplicationStatusById(id));
    }

    /// <summary>
    /// Create application status
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateApplicationStatus([FromBody] ApplicationStatusModel color)
    {
        var newMarkId = await _applicationStatusService.AddApplicationStatus(_mapper.Map<ApplicationStatusDTO>(color));
        return Ok(await _applicationStatusService.GetApplicationStatusById(newMarkId));
    }

    /// <summary>
    /// Delete application status
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteApplicationStatus(int id)
    {
        var applicationStatus = await _applicationStatusService.GetApplicationStatusById(id);
        var roles = await _userService.UserGetRoles(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        if (roles.Any(x => x == "Admin"))
        {
            await _applicationStatusService.DeleteApplicationStatusById(id);
            return Ok();
        }

        return BadRequest("Access denied");
    }
}