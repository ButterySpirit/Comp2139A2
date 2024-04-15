using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Assign1.Models;

namespace Assign1.Tests.Models
{
    public class BookingTests
    {
        [Fact]
        public void Booking_RequiredFields_Valid()
        {
            // Arrange
            var booking = new Booking
            {
                BookingId = 1,
                UserId = "user-id",
                TotalCost = 200.50m,
                BookingDate = DateTime.Today,
                PaymentStatus = "Paid",
                ServiceType = "Flight",
                ServiceID = 101
            };

            var validationContext = new ValidationContext(booking);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(booking, validationContext, results, true);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void Booking_RequiredFields_MissingValues()
        {
            // Arrange
            var booking = new Booking();
            var validationContext = new ValidationContext(booking);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(booking, validationContext, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, v => v.MemberNames.Contains("TotalCost"));
            Assert.Contains(results, v => v.MemberNames.Contains("BookingDate"));
            Assert.Contains(results, v => v.MemberNames.Contains("PaymentStatus"));
            // BookingId is not checked because it is non-nullable and defaults to 0.
        }

        [Theory]
        [InlineData("Flight")]
        [InlineData("Hotel")]
        [InlineData("Rental")]
        public void Booking_ServiceType_AssignedCorrectly(string serviceType)
        {
            // Arrange
            var booking = new Booking { ServiceType = serviceType };

            // Act & Assert
            Assert.Equal(serviceType, booking.ServiceType);
        }

        // Additional tests can include checking for default values, ranges, data formats, etc.
    }
}
