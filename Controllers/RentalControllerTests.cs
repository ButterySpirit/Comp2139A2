using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Assign1.Data;
using Assign1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1.Tests.Controllers
{
    public class RentalControllerTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<DbSet<Rental>> _mockSet;
        private readonly Mock<ILogger<RentalController>> _mockLogger;
        private readonly RentalController _controller;
        private readonly List<Rental> _rentals;

        public RentalControllerTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _mockSet = new Mock<DbSet<Rental>>();
            _mockLogger = new Mock<ILogger<RentalController>>();

            _rentals = new List<Rental>
            {
                new Rental { RentalId = 1, VehicleName = "Car1", State = "Available" },
                new Rental { RentalId = 2, VehicleName = "Car2", State = "Rented" }
            };

            _mockSet.As<IQueryable<Rental>>().Setup(m => m.Provider).Returns(_rentals.AsQueryable().Provider);
            _mockSet.As<IQueryable<Rental>>().Setup(m => m.Expression).Returns(_rentals.AsQueryable().Expression);
            _mockSet.As<IQueryable<Rental>>().Setup(m => m.ElementType).Returns(_rentals.AsQueryable().ElementType);
            _mockSet.As<IQueryable<Rental>>().Setup(m => m.GetEnumerator()).Returns(() => _rentals.GetEnumerator());
            _mockContext.Setup(m => m.Rentals).Returns(_mockSet.Object);

            _controller = new RentalController(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfRentals()
        {
            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Rental>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNotPresent()
        {
            var result = await _controller.Details(0);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithRental()
        {
            var result = await _controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Rental>(viewResult.Model);
            Assert.Equal(1, model.RentalId);
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            var newRental = new Rental { RentalId = 3, VehicleName = "Car3", State = "Available" };

            var result = await _controller.Create(newRental);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_Post_InvalidModel_ReturnsView()
        {
            _controller.ModelState.AddModelError("Error", "Some error");

            var result = await _controller.Edit(1, _rentals.First());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_Post_ValidModel_RedirectsToIndex()
        {
            var result = await _controller.DeleteConfirmed(1);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        // Additional tests here for other actions or scenarios
    }
}
