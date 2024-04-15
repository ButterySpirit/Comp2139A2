using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Assign1.Models;

namespace Assign1.Tests.Models
{
    public class HotelTests
    {
        [Fact]
        public void Hotel_RequiredFields_Valid()
        {
            // Arrange
            var hotel = new Hotel
            {
                HotelId = 1,
                HotelName = "Test Hotel",
                Description = "A description of Test Hotel",
                Address = "123 Test St",
                City = "Test City",
                PostalCode = "12345",
                State = "TS",
                Country = "Testland",
                CostPerNight = 150,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Status = "Open",
                StarRating = 4,
                RoomType = "Suite",
                NumberOfRooms = 100,
                MaxOccupancy = 2,
                PhoneNumber = "123-456-7890",
                CancellationPolicy = "24 hours",
                CheckInTime = "14:00",
                CheckOutTime = "11:00",
                image = "imageurl.jpg"
            };

            var validationContext = new ValidationContext(hotel);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(hotel, validationContext, results, true);

            // Assert
            Assert.True(isValid); // The model should be valid when all required fields are set.
        }

        [Fact]
        public void Hotel_RequiredFields_MissingValues_FailsValidation()
        {
            // Arrange
            var hotel = new Hotel(); // Leaving out required properties to test validation
            var validationContext = new ValidationContext(hotel);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(hotel, validationContext, results, true);

            // Assert
            Assert.False(isValid); // The model should be invalid when required fields are not set.
            Assert.Contains(results, v => v.MemberNames.Contains("HotelName"));
            Assert.Contains(results, v => v.MemberNames.Contains("Description"));
            // ... Assert for other required fields as well.
        }

        // Additional tests can include checking for data types, ranges, string formats, etc.
    }
}
