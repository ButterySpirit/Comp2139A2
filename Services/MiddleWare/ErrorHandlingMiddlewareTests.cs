using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Assign1.Services.Middleware;

namespace Assign1.Tests.Services.Middleware
{
    public class ErrorHandlingMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _mockNext;
        private readonly Mock<ILogger<ErrorHandlingMiddleware>> _mockLogger;
        private readonly ErrorHandlingMiddleware _middleware;

        public ErrorHandlingMiddlewareTests()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            _middleware = new ErrorHandlingMiddleware(_mockNext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task InvokeAsync_CatchesException_SetsResponseStatusCodeTo500()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();

            _mockNext.Setup(n => n(httpContext)).ThrowsAsync(new InvalidOperationException());

            // Act
            await _middleware.InvokeAsync(httpContext);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, httpContext.Response.StatusCode);
            _mockLogger.Verify(logger => logger.LogError(
                It.IsAny<Exception>(),
                It.Is<string>(msg => msg.Contains("An unexpected error occurred!")),
                It.IsAny<object[]>()), Times.Once);
        }
    }
}
