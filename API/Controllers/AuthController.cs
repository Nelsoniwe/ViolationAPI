using API.Models;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthController(IMapper mapper, IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _mapper = mapper;
        _userService = userService;
    }

    /// <summary>
    /// action for user registration
    /// </summary>
    /// <param name="model">user register model</param>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        return new ObjectResult(await _authService.RegisterUser(_mapper.Map<UserDTO>(model), model.Password))
            { StatusCode = StatusCodes.Status201Created };
    }

    /// <summary>
    /// action for user login
    /// </summary>
    /// <param name="model">user login model with email and password</param>
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userService.GetByEmail(model.Email);

        if (user == null || !await _userService.UserCheckPassword(user.Id, model.Password))
            return Unauthorized();

        var loginResult = await _authService.LoginUser(user);
        return Ok(loginResult);
    }
}