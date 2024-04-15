using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Assign1.Data;
using Assign1.Models;
using Assign1.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Assign1.Tests.Controllers
{
    public class AdminControllerTests
    {
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<ILogger<AdminController>> _mockLogger;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _mockLogger = new Mock<ILogger<AdminController>>();

            // Setup the mock context here...
            var flights = GetTestFlights().AsQueryable();

            var mockFlightSet = new Mock<DbSet<Flight>>();
            mockFlightSet.As<IQueryable<Flight>>().Setup(m => m.Provider).Returns(flights.Provider);
            mockFlightSet.As<IQueryable<Flight>>().Setup(m => m.Expression).Returns(flights.Expression);
            mockFlightSet.As<IQueryable<Flight>>().Setup(m => m.ElementType).Returns(flights.ElementType);
            mockFlightSet.As<IQueryable<Flight>>().Setup(m => m.GetEnumerator()).Returns(flights.GetEnumerator());
            mockFlightSet.As<IAsyncEnumerable<Flight>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<Flight>(flights.GetEnumerator()));

            _mockContext.Setup(c => c.Flights).Returns(mockFlightSet.Object);

            _controller = new AdminController(_mockContext.Object, _mockLogger.Object);
        }

        // Your test methods here...

        private List<Flight> GetTestFlights()
        {
            return new List<Flight>
            {
                // Populate with test data
                new Flight { /* properties */ },
                // Additional test flights
            };
        }

        private class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public ValueTask DisposeAsync() => ValueTask.CompletedTask;

            public ValueTask<bool> MoveNextAsync() => ValueTask.FromResult(_inner.MoveNext());

            public T Current => _inner.Current;
        }
    }
}
