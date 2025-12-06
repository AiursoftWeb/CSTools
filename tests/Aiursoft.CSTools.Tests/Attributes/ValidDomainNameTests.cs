using System.ComponentModel.DataAnnotations;
using Aiursoft.CSTools.Attributes;

namespace Aiursoft.CSTools.Tests.Attributes
{
    [TestClass]
    public class ValidDomainNameTests
    {
        [TestMethod]
        public void IsValid_ValidDomainName_ReturnsTrue()
        {
            // Arrange
            var attribute = new ValidDomainName();

            // Act
            var result = attribute.IsValid("valid_domain_name123");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_InvalidDomainName_ReturnsFalse()
        {
            // Arrange
            var attribute = new ValidDomainName();

            // Act
            var result = attribute.IsValid("invalid_domain_name.");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Null_ReturnsFalse()
        {
            // Arrange
            var attribute = new ValidDomainName();

            // Act
            var result = attribute.IsValid(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_ErrorMessageContainsDisplayName()
        {
            // Arrange
            var attribute = new ValidDomainName();
            var value = "invalid_domain_name!";
            var validationContext = new ValidationContext(value) { DisplayName = "TestValue" };

            // Act
            var result = attribute.GetValidationResult(value, validationContext);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ErrorMessage);
            Assert.Contains(validationContext.DisplayName, result.ErrorMessage!);
        }
    }
}
