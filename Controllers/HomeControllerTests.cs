using Xunit;
using Microsoft.AspNetCore.Mvc;
using Assign1.Controllers;
using Assign1.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using System;

namespace Assign1.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> _mockLogger;
        private readonly HomeController _controller;
        private readonly DefaultHttpContext _httpContext;

        public HomeControllerTests()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_mockLogger.Object);
            _httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = _httpContext
            };
        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_ReturnsErrorViewModel_WithRequestId()
        {
            // Arrange
            var expectedPath = "/some/path";
            var expectedException = new Exception("Test exception");

            _httpContext.Features.Set<IExceptionHandlerPathFeature>(new ExceptionHandlerFeature
            {
                Error = expectedException,
                Path = expectedPath
            });

            Activity.Current = new Activity("SomeActivity").Start();

            // Act
            var result = _controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ErrorViewModel>(viewResult.Model);
            Assert.Equal(Activity.Current?.Id, model.RequestId);
            Assert.Equal(expectedPath, model.Path);
            Assert.Equal(expectedException.Message, model.ExceptionMessage);
        }

        [Fact]
        public void Error_ReturnsError404View_WhenStatusCodeIs404()
        {
            // Arrange
            _httpContext.Response.StatusCode = 404;

            // Act
            var result = _controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error404", viewResult.ViewName);
        }

        [Fact]
        public void Error_ReturnsErrorView_WhenStatusCodeIsNot404()
        {
            // Arrange
            _httpContext.Response.StatusCode = 500; // Internal Server Error

            // Act
            var result = _controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }

        // Additional tests here for other actions or scenarios
    }
}
