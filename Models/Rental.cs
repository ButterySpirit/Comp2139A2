using System;
using System.ComponentModel.DataAnnotations;

namespace Assign1.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleType { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int RentalCost { get; set; }
        public bool CarInsurance { get; set; }
        public int VehicleID { get; set; }
        public string LicensePlate { get; set; }
        public string MakeModel { get; set; }
        public int Year { get; set; }
        public double Mileage { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public string Status { get; set; }
        public bool Availability { get; set; }
        public bool GPSIncluded { get; set; }
        public bool AdditionalDriverOption { get; set; }
        public int DriverAge { get; set; }
    }
}
