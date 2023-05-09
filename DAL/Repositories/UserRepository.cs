using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository to work with user info
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Manager for managing users
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userManager">User manager</param>
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public IQueryable<User> GetAll()
        {
            return _userManager.Users;
        }

        public IQueryable<User> GetAllWithDetails()
        {
            return _userManager.Users.Include(x => x.UserProfile);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(Convert.ToString(id));
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await _userManager.Users.Include(x => x.UserProfile).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(User entity)
        {
            var user = await GetByIdWithDetailsAsync(entity.Id);
            if (user != null)
            {
                user.Email = entity.Email;
                user.NormalizedEmail = entity.Email.ToUpper();
                user.UserName = entity.UserName;
                user.NormalizedUserName = entity.UserName.ToUpper();
                user.UserProfile.FirstName = entity.UserProfile.FirstName;
                user.UserProfile.LastName = entity.UserProfile.LastName;
            }
        }

        public async Task<IdentityResult> UserAddAsync(User entity, string password)
        {
            return await _userManager.CreateAsync(entity, password);
            
        }

        public async Task<IdentityResult> UserChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<bool> UserCheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IEnumerable<string>> UserGetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}