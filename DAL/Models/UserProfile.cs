using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class UserProfile
    {
        [ForeignKey("AppUser")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public virtual User AppUser { get; set; }

        public ICollection<Application> Applications { get; set; }

        public UserProfile()
        {
            Applications ??= new HashSet<Application>();
        }
    }
}