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
/// Controller for work with vehicle marks
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VehicleMarkController : ControllerBase
{
    private readonly IVehicleMarkService _vehicleMarkService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public VehicleMarkController(IMapper mapper, IUserService userService, IVehicleMarkService vehicleMarkService)
    {
        _mapper = mapper;
        _userService = userService;
        _vehicleMarkService = vehicleMarkService;
    }

    /// <summary>
    /// Get all marks
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<VehicleMarkDTO>>> GetAllMarks()
    {
        return Ok(await _vehicleMarkService.GetAllVehicleMarks());
    }

    /// <summary>
    /// Get mark by name
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ByName/{name}")]
    public async Task<ActionResult<IEnumerable<VehicleMarkDTO>>> GetMarkByName(string name)
    {
        return Ok(await _vehicleMarkService.GetVehicleMarkByName(name));
    }

    /// <summary>
    /// Get mark by id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ById/{id}")]
    public async Task<ActionResult<IEnumerable<VehicleMarkDTO>>> GetMarkById(int id)
    {
        return Ok(await _vehicleMarkService.GetVehicleMarkById(id));
    }

    /// <summary>
    /// Create mark
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateMark([FromBody] VehicleMarkModel color)
    {  
        await _vehicleMarkService.AddVehicleMark(_mapper.Map<VehicleMarkDTO>(color));
        return Ok(await _vehicleMarkService.GetVehicleMarkByName(color.Type));
    }

    /// <summary>
    /// Delete mark
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteMark(int id)
    {
        var mark = await _vehicleMarkService.GetVehicleMarkById(id);

        await _vehicleMarkService.DeleteVehicleMarkById(id);
        return Ok();
    }
}