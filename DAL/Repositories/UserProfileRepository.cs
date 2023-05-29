using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    /// <summary>
    /// Repository to work with user profiles
    /// </summary>
    public class UserProfileRepository : IUserProfileRepository
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly ViolationContext _db;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">Database context</param>
        public UserProfileRepository(ViolationContext db)
        {
            _db = db;
        }

        public async Task<IQueryable<UserProfile>> GetAllWithDetailsAsync()
        {
            //var users = await _db.UserProfiles
            //    .Include(x => x.Blog)
            //    .Include(x => x.AppUser)
            //    .ToListAsync();
            return _db.UserProfiles;
        }

        public async Task<UserProfile> GetByIdAsync(int id)
        {
            return await _db.UserProfiles.FindAsync(id);
        }

        public async Task<UserProfile> GetByIdWithDetailsAsync(int id)
        {
            return await _db.UserProfiles.Include(x=>x.AppUser).Include(x=>x.Applications).FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}