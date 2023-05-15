using System.Security.Claims;
using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for work with vehicle colors
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VehicleColorController : ControllerBase
{
    private readonly IVehicleColorService _vehicleColorService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public VehicleColorController(IMapper mapper, IUserService userService, IVehicleColorService vehicleColorService)
    {
        _mapper = mapper;
        _userService = userService;
        _vehicleColorService = vehicleColorService;
    }

    /// <summary>
    /// Get all colors
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<VehicleColorDTO>>> GetAllColors()
    {
        return Ok(await _vehicleColorService.GetAllVehicleColors());
    }

    /// <summary>
    /// Get color by name
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("GetByName")]
    public async Task<ActionResult<IEnumerable<VehicleColorDTO>>> GetColorByName(string name)
    {
        return Ok(await _vehicleColorService.GetVehicleColorByName(name));
    }

    /// <summary>
    /// Get color by id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("GetById")]
    public async Task<ActionResult<IEnumerable<VehicleColorDTO>>> GetColorById(int id)
    {
        return Ok(await _vehicleColorService.GetVehicleColorById(id));
    }

    /// <summary>
    /// Create color
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateColor([FromBody] VehicleColorModel color)
    {
        var newColorId = await _vehicleColorService.AddVehicleColor(_mapper.Map<VehicleColorDTO>(color));
        return Ok(await _vehicleColorService.GetVehicleColorById(newColorId));
    }

    /// <summary>
    /// Delete color
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteColor(int id)
    {
        var color = await _vehicleColorService.GetVehicleColorById(id);

        await _vehicleColorService.DeleteVehicleColorById(id);
        return Ok();
    }
}