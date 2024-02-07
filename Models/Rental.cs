using System;
using System.ComponentModel.DataAnnotations;

namespace Assign1.Models
{
    public class Rental
    {
        [Required]
        public int RentalId { get; set; }
        [Required]
        public int RentalCost { get; set; }

        [Required]
        public string VehicleName { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public int CostPerDay { get; set; }

        [Required]
        public bool CarInsurance { get; set; }

        [Required]
        public int VehicleID { get; set; }

        [Required]
        public string? LicensePlate { get; set; }

        [Required]
        public string? MakeModel { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public double Mileage { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string PickupLocation { get; set; }

        [Required]
        public string DropoffLocation { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public bool Availability { get; set; }

        public bool GPSIncluded { get; set; }

        public bool AdditionalDriverOption { get; set; }

        public int DriverAge { get; set; }

        public int BookingID { get; set; }
    }
}
