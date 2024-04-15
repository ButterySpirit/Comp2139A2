using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Assign1.Controllers;
using Assign1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assign1.Tests.Controllers
{
    public class UserRolesControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<ILogger<UserRolesController>> _mockLogger;
        private readonly UserRolesController _controller;
        private readonly List<ApplicationUser> _users;

        public UserRolesControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                roleStoreMock.Object, null, null, null, null);

            _mockLogger = new Mock<ILogger<UserRolesController>>();

            // Sample user data
            _users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", UserName = "user1", Email = "user1@example.com" },
                new ApplicationUser { Id = "2", UserName = "user2", Email = "user2@example.com" }
            };

            _mockUserManager.Setup(um => um.Users).Returns(_users.AsQueryable());

            _controller = new UserRolesController(_mockUserManager.Object, _mockRoleManager.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithUserRolesViewModel()
        {
            // Mock the UserManager GetRolesAsync method
            _mockUserManager.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new List<string> { "Admin" });

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserRolesViewModel>>(viewResult.Model);
            Assert.Equal(2, model.Count()); // Assuming there are 2 users
            Assert.All(model, vm => Assert.Contains("Admin", vm.Roles));
        }

        // More tests to verify that the controller behaves correctly for other actions and error scenarios...
    }
}
