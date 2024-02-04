using System;
using System.ComponentModel.DataAnnotations;

namespace Assign1.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int RentalCost { get; set; }

        [Required]
        public string VehicleName { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public string State { get; set; }
        [Required]

        public string Country { get; set; }

        public int CostPerNight { get; set; }
        public bool CarInsurance { get; set; }

        public int VehicleID { get; set; }

        public string? LicensePlate { get; set; }
        public string? MakeModel { get; set; }
        public int Year { get; set; }
        public double Mileage { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string PickupLocation { get; set; }

        [Required]
        public string DropoffLocation { get; set; }

        [Required]
        public string Status { get; set; }

        public bool Availability { get; set; }

        public bool GPSIncluded { get; set; }

        public bool AdditionalDriverOption { get; set; }

        public int DriverAge { get; set; }

        public int BookingID { get; set; }
    }
}
