using DAL.Models;

namespace DAL.Interfaces;

/// <summary>
/// Repository interface to work with user profiles
/// </summary>
public interface IUserProfileRepository
{
    /// <summary>
    /// Asynchronously gets user
    /// </summary>
    /// <param name="id">Id of user that should be found</param>
    /// <returns>User profile info <see cref="UserProfile"/></returns>
    Task<UserProfile> GetByIdAsync(int id);

    public Task<IQueryable<UserProfile>> GetAllWithDetailsAsync();

    public Task<UserProfile> GetByIdWithDetailsAsync(int id);
}