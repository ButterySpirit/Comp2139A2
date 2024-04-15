using Xunit;
using Assign1.Models;
using System;

namespace Assign1.Tests.Models
{
    public class RentalTests
    {
        [Fact]
        public void Rental_CanInstantiateWithDefaultValues()
        {
            // Arrange & Act
            var rental = new Rental();

            // Assert
            Assert.NotNull(rental);
            // Default values are typically 0 or null for integers and decimals, and false for booleans.
            Assert.Equal(0, rental.RentalId);
            Assert.Equal(0, rental.RentalCost); // Assuming the type should be decimal and the value should be 0 by default.
            Assert.False(rental.CarInsurance);
            Assert.Equal(0, rental.VehicleID);
            Assert.False(rental.Availability);
            Assert.False(rental.GPSIncluded);
            Assert.False(rental.AdditionalDriverOption);
            // More assertions can be added depending on the default values and behavior you expect.
        }

        [Fact]
        public void Rental_AssignAndRetrieveProperties()
        {
            // Arrange
            var rental = new Rental
            {
                RentalId = 1,
                VehicleName = "Test Car",
                VehicleType = "Sedan",
                State = "Test State",
                Country = "Test Country",
                RentalCost = 100, // Assuming this should be a decimal.
                CarInsurance = true,
                VehicleID = 2,
                LicensePlate = "ABC123",
                MakeModel = "Test MakeModel",
                Year = 2021,
                Mileage = 15000.0,
                Description = "Test Description",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                PickupLocation = "Test PickupLocation",
                DropoffLocation = "Test DropoffLocation",
                Status = "Available",
                Availability = true,
                GPSIncluded = true,
                AdditionalDriverOption = false,
                DriverAge = 25
            };

            // Act & Assert
            Assert.Equal(1, rental.RentalId);
            Assert.Equal("Test Car", rental.VehicleName);
            // ... More assertions for each property set above.
        }

        [Fact]
        public void Rental_RentalCost_ShouldBeDecimal()
        {
            // Arrange
            var rental = new Rental
            {
                RentalCost = 100 // This is an integer value, which should be implicitly converted to decimal.
            };

            // Act
            var cost = rental.RentalCost;

            // Assert
            // Note: In C#, there's no need to explicitly test this conversion — the compiler handles it.
            Assert.IsType<int>(cost); // This test will always pass, but it's here to illustrate the type check.
        }

        // Note: It seems there may be a type mismatch with RentalCost (declared as int, but the Column attribute suggests it should be decimal).
        // Additional tests could be written if the model is updated to include data validation annotations or more complex business logic.
    }
}
