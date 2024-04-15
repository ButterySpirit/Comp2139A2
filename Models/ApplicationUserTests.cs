using Xunit;
using Assign1.Models;

namespace Assign1.Tests.Models
{
    public class ApplicationUserTests
    {
        [Fact]
        public void ApplicationUser_CanInstantiateAndSetProperties()
        {
            // Arrange
            var user = new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Preferences = "None",
                FrequentFlyerNumber = "ABC123",
                HotelLoyaltyProgramId = "XYZ789",
                UserName = "johndoe"
            };

            // Act & Assert
            Assert.NotNull(user);
            Assert.Equal("John", user.FirstName);
            Assert.Equal("Doe", user.LastName);
            Assert.Equal("john.doe@example.com", user.Email);
            Assert.Equal("None", user.Preferences);
            Assert.Equal("ABC123", user.FrequentFlyerNumber);
            Assert.Equal("XYZ789", user.HotelLoyaltyProgramId);
            Assert.Equal("johndoe", user.UserName);
            Assert.Equal(10, user.UsernameChangeLimit); // default value
        }

        [Fact]
        public void ApplicationUser_DefaultsUsernameChangeLimitTo10()
        {
            // Arrange & Act
            var user = new ApplicationUser();

            // Assert
            Assert.Equal(10, user.UsernameChangeLimit);
        }

        [Fact]
        public void ApplicationUser_AllowsSettingProfilePicture()
        {
            // Arrange
            var user = new ApplicationUser();
            byte[] profilePictureData = { 0x20, 0x20, 0x20 };

            // Act
            user.ProfilePicture = profilePictureData;

            // Assert
            Assert.Equal(profilePictureData, user.ProfilePicture);
        }

        // Add more tests to verify any other logic or behavior specific to the ApplicationUser model.
    }
}
