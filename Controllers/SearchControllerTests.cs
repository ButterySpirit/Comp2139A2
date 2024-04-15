using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Assign1.Data;
using Assign1.Models;
using Assign1.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Assign1.Tests.Controllers
{
    public class SearchControllerTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<ILogger<SearchController>> _mockLogger;
        private readonly SearchController _controller;
        private readonly List<Hotel> _hotels;
        private readonly List<Flight> _flights;
        private readonly List<Rental> _rentals;

        public SearchControllerTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _mockLogger = new Mock<ILogger<SearchController>>();

            // Mock data for hotels, flights, and rentals
            _hotels = new List<Hotel> { /* Add hotel test data here */ };
            _flights = new List<Flight> { /* Add flight test data here */ };
            _rentals = new List<Rental> { /* Add rental test data here */ };

            // Mock DbSets and their async query providers
            var mockHotelSet = new Mock<DbSet<Hotel>>();
            mockHotelSet.As<IQueryable<Hotel>>().Setup(m => m.Provider).Returns(_hotels.AsQueryable().Provider);
            mockHotelSet.As<IQueryable<Hotel>>().Setup(m => m.Expression).Returns(_hotels.AsQueryable().Expression);
            mockHotelSet.As<IQueryable<Hotel>>().Setup(m => m.ElementType).Returns(_hotels.AsQueryable().ElementType);
            mockHotelSet.As<IQueryable<Hotel>>().Setup(m => m.GetEnumerator()).Returns(() => _hotels.GetEnumerator());

            var mockFlightSet = new Mock<DbSet<Flight>>();
            // ...similar setup for flights

            var mockRentalSet = new Mock<DbSet<Rental>>();
            // ...similar setup for rentals

            _mockContext.Setup(m => m.Hotels).Returns(mockHotelSet.Object);
            _mockContext.Setup(m => m.Flights).Returns(mockFlightSet.Object);
            _mockContext.Setup(m => m.Rentals).Returns(mockRentalSet.Object);

            _controller = new SearchController(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task HotelSearch_ValidQuery_ReturnsPartialViewWithHotels()
        {
            // Arrange
            string destination = "SomeDestination";
            int rating = 3;
            DateTime checkIn = DateTime.Now;
            DateTime checkOut = DateTime.Now.AddDays(1);

            // Act
            var result = await _controller.HotelSearch(destination, rating, checkIn, checkOut);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("_HotelResults", partialViewResult.ViewName);
            var model = Assert.IsAssignableFrom<IEnumerable<Hotel>>(partialViewResult.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task FlightSearch_ValidQuery_ReturnsPartialViewWithFlights()
        {
            // Arrange
            string departure = "SomeDeparture";
            string arrival = "SomeArrival";
            string status = "OnTime";

            // Act
            var result = await _controller.Flight(departure, arrival, status);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("_FlightResults", partialViewResult.ViewName);
            var model = Assert.IsAssignableFrom<IEnumerable<Flight>>(partialViewResult.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task RentalSearch_ValidQuery_ReturnsPartialViewWithRentals()
        {
            // Arrange
            string vehicleName = "SomeVehicleName";
            string vehicleType = "SomeVehicleType";
            string state = "SomeState";

            // Act
            var result = await _controller.Rental(vehicleName, vehicleType, state);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("_RentalResults", partialViewResult.ViewName);
            var model = Assert.IsAssignableFrom<IEnumerable<Rental>>(partialViewResult.Model);
            Assert.NotEmpty(model);
        }

        // Additional tests here for invalid queries and exception handling
    }
}
