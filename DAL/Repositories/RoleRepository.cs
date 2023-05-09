using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository to work with roles
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// Manager for managing roles
        /// </summary>
        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="roleManager">Role manager</param>
        public RoleRepository(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}