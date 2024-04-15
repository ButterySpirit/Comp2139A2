using Xunit;
using Assign1.Models;

namespace Assign1.Tests.Models
{
    public class EditProfileViewModelTests
    {
        [Fact]
        public void EditProfileViewModel_CanSetAndGetPreferences()
        {
            // Arrange
            var viewModel = new EditProfileViewModel();
            var testPreferences = "No preference";

            // Act
            viewModel.Preferences = testPreferences;

            // Assert
            Assert.Equal(testPreferences, viewModel.Preferences);
        }

        [Fact]
        public void EditProfileViewModel_CanSetAndGetFrequentFlyerNumber()
        {
            // Arrange
            var viewModel = new EditProfileViewModel();
            var testFrequentFlyerNumber = "ABC123";

            // Act
            viewModel.FrequentFlyerNumber = testFrequentFlyerNumber;

            // Assert
            Assert.Equal(testFrequentFlyerNumber, viewModel.FrequentFlyerNumber);
        }

        [Fact]
        public void EditProfileViewModel_CanSetAndGetHotelLoyaltyProgramId()
        {
            // Arrange
            var viewModel = new EditProfileViewModel();
            var testHotelLoyaltyProgramId = "XYZ789";

            // Act
            viewModel.HotelLoyaltyProgramId = testHotelLoyaltyProgramId;

            // Assert
            Assert.Equal(testHotelLoyaltyProgramId, viewModel.HotelLoyaltyProgramId);
        }

        [Fact]
        public void EditProfileViewModel_CanHandleNullValues()
        {
            // Arrange
            var viewModel = new EditProfileViewModel();

            // Act & Assert
            Assert.Null(viewModel.Preferences);
            Assert.Null(viewModel.FrequentFlyerNumber);
            Assert.Null(viewModel.HotelLoyaltyProgramId);
        }
    }
}
