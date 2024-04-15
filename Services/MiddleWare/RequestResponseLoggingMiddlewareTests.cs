using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Assign1.Services.Middleware;

namespace Assign1.Tests.Services.Middleware
{
    public class RequestResponseLoggingMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _mockNext;
        private readonly Mock<ILogger<RequestResponseLoggingMiddleware>> _mockLogger;
        private readonly RequestResponseLoggingMiddleware _middleware;

        public RequestResponseLoggingMiddlewareTests()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<RequestResponseLoggingMiddleware>>();
            _middleware = new RequestResponseLoggingMiddleware(_mockNext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task InvokeAsync_LogsRequestAndResponse()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/test";
            context.Response.StatusCode = 200;

            _mockNext.Setup(n => n(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            _mockLogger.Verify(
                logger => logger.LogInformation(
                    "Handling request: {Method} {Url}",
                    It.IsAny<object[]>()),
                Times.Once);

            _mockLogger.Verify(
                logger => logger.LogInformation(
                    "Handled response: {StatusCode}",
                    It.IsAny<object[]>()),
                Times.Once);
        }
    }
}
