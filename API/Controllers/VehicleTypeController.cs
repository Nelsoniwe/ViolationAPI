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
/// Controller for work with vehicle types
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VehicleTypeController : ControllerBase
{
    private readonly IVehicleTypeService _vehicleTypeService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public VehicleTypeController(IMapper mapper, IUserService userService, IVehicleTypeService vehicleTypeService)
    {
        _mapper = mapper;
        _userService = userService;
        _vehicleTypeService = vehicleTypeService;
    }

    /// <summary>
    /// Get all types
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<VehicleTypeDTO>>> GetAllTypes()
    {
        return Ok(await _vehicleTypeService.GetAllVehicleTypes());
    }

    /// <summary>
    /// Get type by name
    /// </summary>
    [HttpGet]
    [Route("ByName/{name}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<VehicleTypeDTO>>> GetTypeByName(string name)
    {
        return Ok(await _vehicleTypeService.GetVehicleTypeByName(name));
    }

    /// <summary>
    /// Get type by id
    /// </summary>
    [HttpGet]
    [Route("ById/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<VehicleTypeDTO>>> GetTypeById(int id)
    {
        return Ok(await _vehicleTypeService.GetVehicleTypeById(id));
    }

    /// <summary>
    /// Create type
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateType([FromBody] VehicleTypeModel color)
    {
        await _vehicleTypeService.AddVehicleType(_mapper.Map<VehicleTypeDTO>(color));
        return Ok();
    }

    /// <summary>
    /// Delete type
    /// </summary>
    [HttpDelete]
    [Route("Delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteType(int id)
    {
        var type = await _vehicleTypeService.GetVehicleTypeById(id);

        await _vehicleTypeService.DeleteVehicleTypeById(id);
        return Ok();
    }
}