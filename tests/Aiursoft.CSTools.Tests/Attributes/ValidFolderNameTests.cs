using System.ComponentModel.DataAnnotations;
using Aiursoft.CSTools.Attributes;

namespace Aiursoft.CSTools.Tests.Attributes
{
    [TestClass]
    public class ValidFolderNameTests
    {
        [TestMethod]
        public void IsValid_ValidFolderName_ReturnsTrue()
        {
            // Arrange
            var attribute = new ValidFolderName();
            var validFolderName = "ValidFolderName";

            // Act
            var result = attribute.IsValid(validFolderName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_InvalidFolderName_ReturnsFalse()
        {
            // Arrange
            var attribute = new ValidFolderName();
            var invalidFolderName = "InvalidFolderName*";

            // Act
            var result = attribute.IsValid(invalidFolderName);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Null_ReturnsFalse()
        {
            // Arrange
            var attribute = new ValidFolderName();

            // Act
            var result = attribute.IsValid(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_ErrorMessageContainsDisplayName()
        {
            // Arrange
            var attribute = new ValidFolderName();
            var value = "InvalidFolderName*";
            var validationContext = new ValidationContext(value) { DisplayName = "FolderName" };

            // Act
            var result = attribute.GetValidationResult(value, validationContext);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage != null && result.ErrorMessage.Contains(validationContext.DisplayName));
        }
    }
}