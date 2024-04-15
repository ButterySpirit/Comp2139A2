using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Assign1.Services.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assign1.Tests.Services.Filters
{
    public class LogUserActivityFilterTests
    {
        private readonly Mock<ILogger<LogUserActivityFilter>> _mockLogger;
        private readonly LogUserActivityFilter _filter;

        public LogUserActivityFilterTests()
        {
            _mockLogger = new Mock<ILogger<LogUserActivityFilter>>();
            _filter = new LogUserActivityFilter(_mockLogger.Object);
        }

        [Fact]
        public void OnActionExecuting_LogsCorrectInformation()
        {
            // Arrange
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()
            );

            actionContext.RouteData.Values["action"] = "TestAction";
            actionContext.RouteData.Values["controller"] = "TestController";

            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                controller: null
            );

            // Add a "searchQuery" to simulate a scenario where this would be logged
            actionExecutingContext.ActionArguments["searchQuery"] = "test query";

            // Act
            _filter.OnActionExecuting(actionExecutingContext);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Starting action TestAction on controller TestController")),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);

            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Search query: test query")),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public void OnActionExecuted_LogsCorrectInformation()
        {
            // Arrange
            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new ActionDescriptor()
            );

            actionContext.RouteData.Values["action"] = "TestAction";
            actionContext.RouteData.Values["controller"] = "TestController";

            var actionExecutedContext = new ActionExecutedContext(
                actionContext,
                new List<IFilterMetadata>(),
                controller: null
            );

            // Act
            _filter.OnActionExecuted(actionExecutedContext);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Completed action TestAction on controller TestController")),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}
