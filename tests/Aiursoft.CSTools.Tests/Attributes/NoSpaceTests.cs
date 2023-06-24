using System.ComponentModel.DataAnnotations;
using Aiursoft.CSTools.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aiursoft.CSTools.Tests.Attributes
{
    [TestClass]
    public class NoSpaceTests
    {
        [TestMethod]
        public void IsValid_NoSpace_ReturnsTrue()
        {
            // Arrange
            var attribute = new NoSpace();

            // Act
            var result = attribute.IsValid("NoSpace");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_ContainsSpace_ReturnsFalse()
        {
            // Arrange
            var attribute = new NoSpace();

            // Act
            var result = attribute.IsValid("Contains Space");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Null_ReturnsFalse()
        {
            // Arrange
            var attribute = new NoSpace();

            // Act
            var result = attribute.IsValid(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_ErrorMessageContainsDisplayName()
        {
            // Arrange
            var attribute = new NoSpace();
            var value = "Contains Space";
            var validationContext = new ValidationContext(value) { DisplayName = "TestValue" };

            // Act
            var result = attribute.GetValidationResult(value, validationContext);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ErrorMessage != null && result.ErrorMessage.Contains(validationContext.DisplayName));
        }
    }
}