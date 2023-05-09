namespace DAL.Interfaces;

public interface IRoleRepository
{
    /// <summary>
    /// Check if role exist or not
    /// </summary>
    /// <param name="roleName">Role name</param>
    /// <returns>true if role exist and false if not</returns>
    Task<bool> RoleExistsAsync(string roleName);
}