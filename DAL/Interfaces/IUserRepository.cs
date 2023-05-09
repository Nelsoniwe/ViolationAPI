using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Interfaces;

/// <summary>
/// Repository interface to work with user info
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets all users from db
    /// </summary>
    /// <returns><see cref="IQueryable"/> of <see cref="User"/></returns>
    IQueryable<User> GetAll();

    /// <summary>
    /// Gets all users from db with details
    /// </summary>
    /// <returns><see cref="IQueryable"/> of <see cref="User"/></returns>GetAllWithDetails
    IQueryable<User> GetAllWithDetails();

    /// <summary>
    /// Asynchronously gets user by id from db
    /// </summary>
    /// <param name="id">Id of user</param>
    /// <returns><see cref="User"/> info</returns>
    Task<User> GetByIdAsync(int id);

    /// <summary>
    /// Asynchronously gets user by email from db
    /// </summary>
    /// <param name="email">user email</param>
    /// <returns><see cref="User"/> info</returns>
    Task<User> GetByEmailAsync(string email);

    /// <summary>
    /// Asynchronously compare passed password with users db password
    /// </summary>
    /// <param name="user"><see cref="User"/> whose password compared</param>
    /// <param name="password">Password that we compare</param>
    /// <returns>true if passwords matches and false if not</returns>
    Task<bool> UserCheckPasswordAsync(User user, string password);

    /// <summary>
    /// Asynchronously adds user to db
    /// </summary>
    /// <param name="user"><see cref="User"/> info about user</param>
    /// <param name="password">user password</param>
    /// <returns><see cref="IdentityResult"/> operation result</returns>
    Task<IdentityResult> UserAddAsync(User user, string password);

    /// <summary>
    /// Asynchronously updates user info
    /// </summary>
    /// <param name="user"><see cref="User"/> info with new info</param>
    Task UpdateAsync(User user);

    /// <summary>
    /// Asynchronously deletes user info
    /// </summary>
    /// <param name="id">User id to delete</param>
    Task DeleteByIdAsync(int id);

    /// <summary>
    /// Asynchronously gets user with details by id from db
    /// </summary>
    /// <param name="id">Id of user</param>
    /// <returns><see cref="User"/> info with details</returns>
    Task<User> GetByIdWithDetailsAsync(int id);

    /// <summary>
    /// Asynchronously add role to user
    /// </summary>
    /// <param name="user"> <see cref="User"/> that add to role</param>
    /// <param name="role">Role that add to user</param>
    /// <returns><see cref="IdentityResult"/> operation result</returns>
    Task<IdentityResult> AddToRoleAsync(User user, string role);

    /// <summary>
    /// Asynchronously gets all roles of user
    /// </summary>
    /// <param name="user"> <see cref="User"/> that roles should be returned</param>
    /// <returns><see cref="IEnumerable{T}"/> of roles</returns>
    Task<IEnumerable<string>> UserGetRolesAsync(User user);

    /// <summary>
    /// Asynchronously sets new password to user
    /// </summary>
    /// <param name="user"><see cref="User"/> that password should be updated</param>
    /// <param name="currentPassword">current password</param>
    /// <param name="newPassword">new password</param>
    /// <returns><see cref="IdentityResult"/> operation result</returns>
    Task<IdentityResult> UserChangePasswordAsync(User user, string currentPassword, string newPassword);
}