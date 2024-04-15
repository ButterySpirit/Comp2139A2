using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Assign1.Data;
using Assign1.Models;
using Assign1.Controllers;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Assign1.Tests.Controllers
{
    public class BookingControllerTests
    {
        private readonly BookingController _controller;
        private readonly Mock<DbSet<Booking>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;

        public BookingControllerTests()
        {
            _mockSet = new Mock<DbSet<Booking>>();
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());

            var bookings = new List<Booking>
            {
                new Booking { BookingId = 1, UserId = "user-id", TotalCost = 100, BookingDate = DateTime.Now, PaymentStatus = "Paid", ServiceType = "Hotel", ServiceID = 1 },
                new Booking { BookingId = 2, UserId = "user-id", TotalCost = 200, BookingDate = DateTime.Now, PaymentStatus = "Unpaid", ServiceType = "Flight", ServiceID = 2 }
            }.AsQueryable();

            _mockSet.As<IAsyncEnumerable<Booking>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<Booking>(bookings.GetEnumerator()));
            _mockSet.As<IQueryable<Booking>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Booking>(bookings.Provider));
            _mockSet.As<IQueryable<Booking>>().Setup(m => m.Expression).Returns(bookings.Expression);
            _mockSet.As<IQueryable<Booking>>().Setup(m => m.ElementType).Returns(bookings.ElementType);
            _mockSet.As<IQueryable<Booking>>().Setup(m => m.GetEnumerator()).Returns(bookings.GetEnumerator());

            _mockContext.Setup(m => m.Bookings).Returns(_mockSet.Object);

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "user-id") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller = new BookingController(_mockContext.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = claimsPrincipal } }
            };
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithAListOfBookings()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Booking>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        // Additional test methods...
    }

    // Utility classes to support async queries
    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            var resultType = typeof(TResult).GetGenericArguments()[0];
            var executionResult = typeof(IQueryProvider)
                                     .GetMethod(
                                         name: nameof(IQueryProvider.Execute),
                                         genericParameterCount: 1,
                                         types: new[] { typeof(Expression) })
                                     .MakeGenericMethod(resultType)
                                     .Invoke(this, new[] { expression });

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                                        ?.MakeGenericMethod(resultType)
                                        .Invoke(null, new[] { executionResult });
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return ValueTask.FromResult(_inner.MoveNext());
        }

        public T Current => _inner.Current;

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
