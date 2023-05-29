using System.Security.Claims;
using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for work with violations
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ViolationController : ControllerBase
{
    private readonly IViolationService _violationService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ViolationController(IMapper mapper, IUserService userService, IViolationService violationService)
    {
        _mapper = mapper;
        _userService = userService;
        _violationService = violationService;
    }

    /// <summary>
    /// Get all violation
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ViolationDTO>>> GetAllViolations()
    {
        return Ok(await _violationService.GetAllViolations());
    }

    /// <summary>
    /// Get violation by name
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ByName/{name}")]
    public async Task<ActionResult<IEnumerable<ViolationDTO>>> GetViolationByName(string name)
    {
        return Ok(await _violationService.GetViolationByName(name));
    }

    /// <summary>
    /// Get violation by id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ById/{id}")]
    public async Task<ActionResult<IEnumerable<ViolationDTO>>> GetViolationById(int id)
    {
        return Ok(await _violationService.GetViolationById(id));
    }

    /// <summary>
    /// Create violation
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateViolation([FromBody] ViolationModel color)
    {
        await _violationService.AddViolation(_mapper.Map<ViolationDTO>(color));
        return Ok();
    }

    /// <summary>
    /// Delete violation
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteViolation(int id)
    {
        var violation = await _violationService.GetViolationById(id);
        var roles = await _userService.UserGetRoles(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        await _violationService.DeletViolationById(id);
        return Ok();

    }
}