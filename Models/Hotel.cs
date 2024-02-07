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

        [Required]
        public int CostPerNight { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int StarRating { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required]
        public int NumberOfRooms { get; set; }

        [Required]
        public int MaxOccupancy { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string CancellationPolicy { get; set; }

        [Required]
        public string CheckInTime { get; set; }

        [Required]
        public string CheckOutTime { get; set; }
    }

}

