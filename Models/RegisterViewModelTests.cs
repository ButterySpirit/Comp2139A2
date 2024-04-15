using Xunit;
using System.ComponentModel.DataAnnotations;
using Assign1.Models;
using System.Collections.Generic;

namespace Assign1.Tests.Models
{
    public class RegisterViewModelTests
    {
        [Fact]
        public void RegisterViewModel_RequiredFields_Valid()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Username = "newuser",
                Email = "newuser@example.com",
                Password = "Test@1234",
                ConfirmPassword = "Test@1234",
                Role = "User"
            };

            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.True(isValid); // The model should be valid when all required fields are set correctly.
        }

        [Fact]
        public void RegisterViewModel_EmailAddress_FormatValidation()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Username = "newuser",
                Email = "not-an-email",
                Password = "Test@1234",
                ConfirmPassword = "Test@1234",
                Role = "User"
            };

            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid); // The model should be invalid because the email format is incorrect.
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(RegisterViewModel.Email)));
        }

        [Fact]
        public void RegisterViewModel_Password_StrengthValidation()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Username = "newuser",
                Email = "newuser@example.com",
                Password = "short",
                ConfirmPassword = "short",
                Role = "User"
            };

            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid); // The model should be invalid because the password is too short.
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(RegisterViewModel.Password)));
        }

        [Fact]
        public void RegisterViewModel_ConfirmPassword_MatchValidation()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Username = "newuser",
                Email = "newuser@example.com",
                Password = "Test@1234",
                ConfirmPassword = "DifferentPassword",
                Role = "User"
            };

            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            // Act
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("password and confirmation password do not match"));
        }

        [Fact]
        public void RegisterViewModel_Role_SelectionValidation()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Username = "newuser",
                Email = "newuser@example.com",
                Password = "Test@1234",
                ConfirmPassword = "Test@1234",
                Role = null // Intentionally setting this to null to check the required validation
            };

            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid); // The model should be invalid because the role is not set.
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(RegisterViewModel.Role)));
        }

        // Note: The SelectListItems are not directly validated here, but you might add tests to ensure
        // they are being populated correctly if that logic is part of your codebase.
    }
}
