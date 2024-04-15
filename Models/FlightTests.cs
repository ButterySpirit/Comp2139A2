using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Assign1.Models;

namespace Assign1.Tests.Models
{
    public class FlightTests
    {
        [Fact]
        public void Flight_RequiredFields_Valid()
        {
            // Arrange
            var flight = new Flight
            {
                FlightId = 1,
                AirlineName = "Test Airline",
                DeparturePort = "ABC",
                ArrivalPort = "XYZ",
                AvailSeats = 30,
                TicketCost = 199.99m,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Status = "OnTime",
                FlightDuration = 120
            };

            var validationContext = new ValidationContext(flight);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(flight, validationContext, results, true);

            // Assert
            Assert.True(isValid); // The model should be valid when all required fields are set.
        }

        [Fact]
        public void Flight_RequiredFields_MissingValues_FailsValidation()
        {
            // Arrange
            var flight = new Flight();
            var validationContext = new ValidationContext(flight);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(flight, validationContext, results, true);

            // Assert
            Assert.False(isValid); // The model should be invalid when required fields are not set.
            Assert.Contains(results, v => v.MemberNames.Contains("AirlineName"));
            Assert.Contains(results, v => v.MemberNames.Contains("DeparturePort"));
            // ... Assert for other required fields as well.
        }

        // You may also want to write tests that ensure the StartDate is before the EndDate, etc.
        // Additional tests can include checking for numeric ranges, correct handling of decimal places for TicketCost, etc.
    }
}
