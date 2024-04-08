using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Assign1.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        // Additional user details can go here

        public IList<string> Roles { get; set; }

        public UserRolesViewModel()
        {
            Roles = new List<string>();
        }
    }
}
