using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Assign1.Data;
using Assign1.Models;
using Assign1.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Assign1.Tests.Controllers
{
    public class FlightControllerTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<DbSet<Flight>> _mockSet;
        private readonly Mock<ILogger<FlightController>> _mockLogger;
        private readonly FlightController _controller;
        private readonly List<Flight> _flights;

        public FlightControllerTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _mockSet = new Mock<DbSet<Flight>>();
            _mockLogger = new Mock<ILogger<FlightController>>();

            _flights = new List<Flight>
            {
                new Flight { FlightId = 1, AirlineName = "Airline1", DeparturePort = "A", ArrivalPort = "B", TicketCost = 200, Status = "OnTime" },
                new Flight { FlightId = 2, AirlineName = "Airline2", DeparturePort = "C", ArrivalPort = "D", TicketCost = 300, Status = "Delayed" }
            };

            _mockSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(_flights.AsQueryable().Provider);
            _mockSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(_flights.AsQueryable().Expression);
            _mockSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(_flights.AsQueryable().ElementType);
            _mockSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(() => _flights.GetEnumerator());

            _mockContext.Setup(m => m.Flights).Returns(_mockSet.Object);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            }));

            _controller = new FlightController(_mockContext.Object, _mockLogger.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = claimsPrincipal }
                }
            };
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfFlights()
        {
            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Flight>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNotPresent()
        {
            var result = await _controller.Details(0);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithFlight()
        {
            var result = await _controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Flight>(viewResult.Model);
            Assert.Equal(1, model.FlightId);
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            var newFlight = new Flight { FlightId = 3, AirlineName = "Airline3", DeparturePort = "E", ArrivalPort = "F", TicketCost = 400, Status = "OnTime" };

            var result = await _controller.Create(newFlight);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_Post_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Error", "Some error");

            var result = await _controller.Edit(1, _flights.First());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_Post_ValidModel_RedirectsToIndex()
        {
            var result = await _controller.DeleteConfirmed(1);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        // Additional tests here for Edit, Create, Delete, etc.
    }
}
