using System;
using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class User : IdentityUser<int>
    {
        public UserProfile? UserProfile { get; set; }
    }
}