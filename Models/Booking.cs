using System.ComponentModel.DataAnnotations;
namespace Assign1.Models
{
    public class Booking
    {
        [Required]
        public int BookingId { get; set; }

        public int? UserId { get; set; } // Keep nullable for guest users

        [Required]
        public int TotalCost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        // New fields for service type and ID
        public string ServiceType { get; set; } // 'Flight', 'Hotel', or 'Rental'
        public int? ServiceID { get; set; } // Nullable for flexibility

        // Remove individual service ID properties and their navigation properties
    }
}