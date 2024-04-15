using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assign1.Controllers;
using Assign1.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Assign1.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<ILogger<UserController>> _mockLogger;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            // Mock setup for UserManager
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                null, null, null, null, null, null, null, null);

            // Mock setup for SignInManager
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                _mockUserManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                null, null, null);

            // Mock setup for RoleManager
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                null, null, null, null);

            _mockLogger = new Mock<ILogger<UserController>>();

            // Initialize the controller with the mocked dependencies
            _controller = new UserController(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _mockRoleManager.Object,
                _mockLogger.Object);

            // Mock the Roles for RoleManager
            _mockRoleManager.Setup(r => r.Roles)
                .Returns(new List<IdentityRole> { new IdentityRole("User"), new IdentityRole("Admin") }.AsQueryable());
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
        public void Register_ReturnsAViewResult_WithRoles()
        {
            // Act
            var result = _controller.Register();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<RegisterViewModel>(viewResult.Model);
            Assert.NotNull(model.RolesList);
            Assert.NotEmpty(model.RolesList);
        }

        [Fact]
        public async Task Register_ValidModel_CreatesUserAndRedirectsToLogin()
        {
            // Arrange
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockSignInManager.Setup(sm => sm.SignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var model = new RegisterViewModel
            {
                Username = "newuser",
                Email = "newuser@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                Role = "User"
            };

            // Act
            var result = await _controller.Register(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Login_ValidModel_RedirectsToHome()
        {
            // Arrange
            _mockSignInManager.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var model = new LoginViewModel
            {
                UsernameOrEmail = "user@example.com",
                Password = "Password123!",
                RememberMe = false
            };

            // Act
            var result = await _controller.Login(model, null);

            // Assert
            var redirectResult = Assert.IsType<LocalRedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
        }

        [Fact]
        public async Task Logout_SignsOutAndRedirectsToIndex()
        {
            // Arrange
            _mockSignInManager.Setup(sm => sm.SignOutAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Logout();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        // Additional tests for error cases, failed logins, etc., can be added here.
    }
}
