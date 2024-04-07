using System;
using Microsoft.AspNetCore.Identity;

namespace Assign1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int UsernameChangeLimit { get; set; } = 10;

        public byte[]? ProfilePicture { get; set; }
    }
}
