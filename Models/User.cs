using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Assign1.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public User()
        {
            Bookings = new HashSet<Booking>();
        }
    }
}
