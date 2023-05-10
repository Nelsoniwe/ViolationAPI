using BLL.Models;

namespace BLL.Interfaces;

public interface IUserProfileService
{
    Task<IEnumerable<UserProfileDTO>> GetAllUserProfilesWithDetails();
    Task<UserProfileDTO> GetUserProfileByIdWithDetails(int id);
    Task<UserProfileDTO> GetByUserName(string name);
    Task<UserProfileDTO> GetUserProfileById(int id);
    Task UpdateUserProfileAsync(UserProfileDTO item);
}