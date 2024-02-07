using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Assign1.Models
{
    public class Booking
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign key from User class

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        // Relationships with Flight, Hotel, Rental
        public virtual ICollection<Flight> Flights { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }

        // Constructor to initialize the collections
        public Booking()
        {
            Flights = new HashSet<Flight>();
            Hotels = new HashSet<Hotel>();
            Rentals = new HashSet<Rental>();
        }
    }
}
