using BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces;

public interface IUserService
{
    IEnumerable<UserDTO> GetAll();
    Task<UserDTO> GetById(int id);
    Task<UserDTO> GetByEmail(string email);
    Task<bool> UserCheckPassword(int userId, string password);
    Task CreateUserAndAddToRole(UserDTO user, string password, string role);
    Task UpdateUser(UserDTO user);
    Task DeleteById(int id);
    Task<UserDTO> GetByIdWithDetails(int id);
    Task<IdentityResult> AddToRole(int userId, string role);
    Task<IEnumerable<string>> UserGetRoles(int userId);
    Task<IdentityResult> UserChangePassword(int userId, string currentPassword, string newPassword);
}