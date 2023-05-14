using BLL.Models;

namespace BLL.Interfaces;

/// <summary>
/// Service interface to work with authentication
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="user">User information</param>
    /// <param name="password">User password</param>
    /// <returns><see cref="UserProfileDTO"/> new user profile information</returns>
    Task<UserProfileDTO> RegisterUser(UserDTO user, string password);

    /// <summary>
    /// Login user and generate JWT
    /// </summary>
    /// <param name="user">User information</param>
    /// <returns><see cref="UserLoginDataDTO"/> User id with token</returns>
    Task<UserLoginDataDTO> LoginUser(UserDTO user);
}