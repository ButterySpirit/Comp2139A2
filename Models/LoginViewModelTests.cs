using Xunit;
using System.ComponentModel.DataAnnotations;
using Assign1.Models;
using System.Collections.Generic;

namespace Assign1.Tests.Models
{
    public class LoginViewModelTests
    {
        [Fact]
        public void LoginViewModel_RequiredFields_Valid()
        {
            // Arrange
            var model = new LoginViewModel
            {
                UsernameOrEmail = "user@example.com",
                Password = "P@ssw0rd!"
            };

            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid); // The model should be valid when all required fields are set.
        }

        [Fact]
        public void LoginViewModel_RequiredFields_MissingValues_FailsValidation()
        {
            // Arrange
            var model = new LoginViewModel(); // Missing required properties
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid); // The model should be invalid when required fields are not set.
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(LoginViewModel.UsernameOrEmail)));
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(LoginViewModel.Password)));
        }

        [Fact]
        public void LoginViewModel_RememberMe_DefaultsToFalse()
        {
            // Arrange
            var model = new LoginViewModel();

            // Assert
            Assert.False(model.RememberMe); // The RememberMe should default to false.
        }

        // Additional tests can be added if there are more rules or logic in the ViewModel.
    }
}
