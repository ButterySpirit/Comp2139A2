using System;
using System.ComponentModel.DataAnnotations;

namespace Assign1.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }

        [Required]
        public string HotelName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        public int CostPerNight { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int StarRating { get; set; }

        public string RoomType { get; set; }

        public int NumberOfRooms { get; set; }

        public int MaxOccupancy { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string CancellationPolicy { get; set; }

        public string CheckInTime { get; set; }

        public string CheckOutTime { get; set; }
    }

}

