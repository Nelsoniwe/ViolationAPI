﻿using System.Security.Claims;
using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for work with applications
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ApplicationController(IMapper mapper, IUserService userService, IApplicationService applicationService)
    {
        _mapper = mapper;
        _userService = userService;
        _applicationService = applicationService;
    }

    /// <summary>
    /// Get all applications
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetAllApplications()
    {
        return Ok(await _applicationService.GetAllApplications());
    }

    /// <summary>
    /// Get all applications
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ByFilter")]
    public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetApplicationByFilter([FromQuery] ApplicationFilter filter)
    {
        return Ok(await _applicationService.GetApplicationsByFilter(_mapper.Map<ApplicationFilterDTO>(filter)));
    }

    /// <summary>
    /// Get all applications by status id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ByStatusId/{id}")]
    public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetAllApplicationsByStatusId(int id)
    {
        return Ok(await _applicationService.GetAllApplicationsByStatusId(id));
    }

    /// <summary>
    /// Get all applications by user id
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ByUserId/{id}")]
    public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetAllApplicationsByUserId(int id)
    {
        return Ok(await _applicationService.GetAllUserApplications(id));
    }

    /// <summary>
    /// Get all applications by vehicle number
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    [Route("ByVehicleNumber/{number}")]
    public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetAllApplicationsByVehicleNumber(string number)
    {
        return Ok(await _applicationService.GetAllVehicleApplications(number));
    }

    /// <summary>
    /// Create application
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> CreateApplication([FromBody] ApplicationModel application)
    {
        await _applicationService.AddApplication(_mapper.Map<ApplicationDTO>(application));
        return Ok();
    }

    /// <summary>
    /// Delete application
    /// </summary>
    [HttpDelete]
    [Authorize(Roles = "User")]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteApplication(int id)
    {
        var applicationToDelete = await _applicationService.GetApplicationById(id);
        var roles = await _userService.UserGetRoles(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        if (roles.Any(x => x == "Admin"))
        {
            await _applicationService.DeleteApplicationById(id);
            return Ok();
        }

        var userApplications = await _applicationService.GetAllUserApplications(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        if (!userApplications.Select(x=>x.Id).Contains(id))
        {
            return BadRequest("It is not your application");
        }

        await _applicationService.DeleteApplicationById(id);
        return Ok();
    }

    /// <summary>
    /// Update application by authorized user
    /// </summary>
    /// <param name="application">Application info that should be updated</param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> UpdateApplication([FromBody] ApplicationModel application)
    {
        var roles = await _userService.UserGetRoles(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        if (roles.Any(x => x == "Admin"))
        {
            await _applicationService.UpdateApplicationAsync(_mapper.Map<ApplicationDTO>(application));
            return Ok();
        }

        if (application.UserId != Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        {
            return BadRequest("It is not your application");
        }

        await _applicationService.UpdateApplicationAsync(_mapper.Map<ApplicationDTO>(application));
        return Ok();
    }
}