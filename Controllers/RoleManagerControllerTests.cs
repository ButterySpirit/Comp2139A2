using Xunit;
using Microsoft.AspNetCore.Identity;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Assign1.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assign1.Tests.Controllers
{
    public class RoleManagerControllerTests
    {
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<ILogger<RoleManagerController>> _mockLogger;
        private readonly RoleManagerController _controller;
        private readonly List<IdentityRole> _roles;

        public RoleManagerControllerTests()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            _mockLogger = new Mock<ILogger<RoleManagerController>>();

            _roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin" },
                new IdentityRole { Name = "User" }
            };

            // Mocking RoleManager's Roles property to return a set of roles
            _mockRoleManager.Setup(rm => rm.Roles).Returns(_roles.AsQueryable());

            // Mocking RoleManager's CreateAsync method
            _mockRoleManager.Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            _controller = new RoleManagerController(_mockRoleManager.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfRoles()
        {
            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<IdentityRole>>(viewResult.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task AddRole_ValidRole_RedirectsToIndex()
        {
            var result = await _controller.AddRole("NewRole");

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task AddRole_InvalidRole_ReturnsToIndexWithError()
        {
            _mockRoleManager.Setup(rm => rm.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            var result = await _controller.AddRole("");

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Role name cannot be empty.", _controller.TempData["Error"]);
        }

        // Additional tests here for other scenarios like empty role names, or errors during role creation.
    }
}
