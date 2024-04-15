using Xunit;
using Assign1.Models;
using System.Collections.Generic;

namespace Assign1.Tests.Models
{
    public class UserRolesViewModelTests
    {
        [Fact]
        public void UserRolesViewModel_InstantiatesWithEmptyRolesList()
        {
            // Arrange & Act
            var viewModel = new UserRolesViewModel();

            // Assert
            Assert.NotNull(viewModel.Roles);
            Assert.Empty(viewModel.Roles);
        }

        [Fact]
        public void UserRolesViewModel_CanAddRolesToRolesList()
        {
            // Arrange
            var viewModel = new UserRolesViewModel();
            var role = "User";

            // Act
            viewModel.Roles.Add(role);

            // Assert
            Assert.Contains(role, viewModel.Roles);
            Assert.Single(viewModel.Roles);
        }

        [Fact]
        public void UserRolesViewModel_CanStoreMultipleRoles()
        {
            // Arrange
            var viewModel = new UserRolesViewModel();
            var roles = new List<string> { "Admin", "User", "Guest" };

            // Act
            foreach (var role in roles)
            {
                viewModel.Roles.Add(role);
            }

            // Assert
            Assert.Equal(roles.Count, viewModel.Roles.Count);
            for (int i = 0; i < roles.Count; i++)
            {
                Assert.Equal(roles[i], viewModel.Roles[i]);
            }
        }

        // Additional tests can be added to verify the behavior related to user details if necessary.
    }
}
