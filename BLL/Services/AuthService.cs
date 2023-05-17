using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using BLL.Utility;
using DAL.Interfaces.BaseInterfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

/// <summary>
/// Service to work with authentication
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _db;
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public AuthService(IUnitOfWork uow, IUserService service, IMapper mapper)
    {
        _db = uow;
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Login user and generate JWT
    /// </summary>
    /// <param name="user">User information</param>
    /// <returns><see cref="UserLoginDataDTO"/> User id with token</returns>
    /// <exception cref="NotFoundException">Throws when user not found in db</exception>
    public async Task<UserLoginDataDTO> LoginUser(UserDTO user)
    {
        var symmetricSecurityKey = JwtAuthOptions.GetSymmetricSecurityKey();
        var userFromDb = await _db.UserRepository.GetAll().FirstOrDefaultAsync(x => x.Email == user.Email);

        if (userFromDb == null)
            throw new NotFoundException("User not found");

        var userRoles = await _service.UserGetRoles(userFromDb.Id);
        var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userFromDb.UserName),
                new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString())
            };
        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var claimsIdentity = new ClaimsIdentity(authClaims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: JwtAuthOptions.Issuer,
            audience: JwtAuthOptions.Audience,
            claims: claimsIdentity.Claims,
            expires: now.AddDays(1),
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new UserLoginDataDTO(encodedJwt, userFromDb.Id, userRoles.ToList());
    }
    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="user">User information</param>
    /// <param name="password">User password</param>
    /// <returns><see cref="UserProfileDTO"/> new user profile information</returns>
    public async Task<UserProfileDTO> RegisterUser(UserDTO newUser, string password)
    {
        await _service.CreateUserAndAddToRole(newUser, password, "User");
        var user = await _service.GetByEmail(newUser.Email);
        return _mapper.Map<UserProfileDTO>(user.UserProfile);
    }
}