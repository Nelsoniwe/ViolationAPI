using System.Security.Claims;
using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller for work with users
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserProfileService _userProfileService;
    private readonly IMapper _mapper;

    public UserController(IMapper mapper, IUserService userService, IUserProfileService userProfileService)
    {
        _userService = userService;
        _userProfileService = userProfileService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all user profiles
    /// </summary>
    [HttpGet]
    [Route("GetAll")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<UserProfileDTO>>> GetAllProfiles()
    {
        return Ok(await _userProfileService.GetAllUserProfilesWithDetails());
    }

    /// <summary>
    /// Add role to user
    /// </summary>
    /// <param name="id">User id who should be added to role</param>
    /// <param name="role">Role that should be added to user</param>
    [HttpPost]
    [Route("AddToRole/{id}/{role}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AddUserToRole(int id, string role)
    {
        await _userService.AddToRole(id, role);
        return Ok();
    }

    /// <summary>
    /// Get user profile by id
    /// </summary>
    /// <param name="id">Id of user who should be found</param>
    [HttpGet]
    [Route("GetProfile/{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<UserProfileDTO>> GetFullUserProfileById(int id)
    {
        var userProfile = await _userProfileService.GetUserProfileByIdWithDetails(id);
        return Ok(userProfile);
    }

    /// <summary>
    /// Get user profile by username
    /// </summary>
    /// <param name="username">username of user who should be found</param>
    [HttpGet]
    [Route("GetByUserName")]
    [AllowAnonymous]
    public async Task<ActionResult<UserProfileDTO>> GetByUserName(string username)
    {
        return Ok(await _userProfileService.GetByUserName(username));
    }

    /// <summary>
    /// Get roles of authorized user
    /// </summary>
    [HttpGet]
    [Route("GetRoles")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<string>>> GetRoles()
    {
        return Ok(await _userService.UserGetRoles(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value)));
    }

    /// <summary>
    /// Get user roles by its id
    /// </summary>
    /// <param name="id">User id whose roles should be found</param>
    [HttpGet]
    [Route("GetRoles/{id}")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<string>>> GetRoles(int id)
    {
        return Ok(await _userService.UserGetRoles(id));
    }

    /// <summary>
    /// Get profile of authorized user
    /// </summary>
    [HttpGet]
    [Route("GetProfile")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<UserProfileDTO>> GetProfile()
    {
        var userProfile = await _userProfileService.GetUserProfileByIdWithDetails(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        return Ok(userProfile);
    }

    /// <summary>
    /// Update user by id
    /// </summary>
    /// <param name="userData">User info with old and new values</param>
    /// <param name="id">User id that should be updated</param>
    [HttpPut]
    [Route("UpdateUser/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserProfileDTO>> UpdateUser([FromBody] UserModel userData, int id)
    {
        var userToUpdate = _mapper.Map<UserDTO>(userData);
        userToUpdate.Id = id;
        await _userService.UpdateUser(userToUpdate);
        var userProfile = await _userProfileService.GetUserProfileByIdWithDetails(id);
        return Ok(userProfile);
    }

    /// <summary>
    /// Update authorized user
    /// </summary>
    /// <param name="userData">User info with old and new values</param>
    [HttpPut]
    [Route("UpdateUser")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<UserProfileDTO>> UpdateUser([FromBody] UserModel userData)
    {
        var userToUpdate = _mapper.Map<UserDTO>(userData);
        userToUpdate.Id = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        await _userService.UpdateUser(userToUpdate);
        var userProfile = await _userProfileService.GetUserProfileByIdWithDetails(userToUpdate.Id);
        return Ok(userProfile);
    }

    /// <summary>
    /// Change password for authorized user
    /// </summary>
    /// <param name="passwordModel">PasswordModel with new and old passwords</param>
    [HttpPut]
    [Route("ChangePassword")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> ChangeUserPassword([FromBody] ChangePasswordModel passwordModel)
    {
        await _userService.UserChangePassword(
            Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value),
            passwordModel.CurrentPassword,
            passwordModel.NewPassword);
        return Ok();
    }

    /// <summary>
    /// Delete user by id
    /// </summary>
    /// <param name="id">User id that should be deleted</param>
    [HttpDelete]
    [Route("Delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _userService.DeleteById(id);
        return Ok();
    }

    /// <summary>
    /// Delete authorized user
    /// </summary>
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> DeleteUser()
    {
        await _userService.DeleteById(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        return Ok();
    }
}