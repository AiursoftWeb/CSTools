using System.ComponentModel.DataAnnotations;
using Aiursoft.CSTools.Attributes;

namespace Aiursoft.CSTools.Tests.Attributes
{
    [TestClass]
    public class NoDotTests
    {
        [TestMethod]
        public void IsValid_NoDot_ReturnsTrue()
        {
            // Arrange
            var attribute = new NoDot();

            // Act
            var result = attribute.IsValid("NoDot");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValid_ContainsDot_ReturnsFalse()
        {
            // Arrange
            var attribute = new NoDot();

            // Act
            var result = attribute.IsValid("Contains.Dot");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_Null_ReturnsFalse()
        {
            // Arrange
            var attribute = new NoDot();

            // Act
            var result = attribute.IsValid(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValid_ErrorMessageContainsDisplayName()
        {
            // Arrange
            var attribute = new NoDot();
            var value = "Contains.Dot";
            var validationContext = new ValidationContext(value) { DisplayName = "TestValue" };

            // Act
            var result = attribute.GetValidationResult(value, validationContext);

            // Assert
            Assert.IsNotNull(result);
            #pragma warning disable CS8602
            Assert.IsTrue(result.ErrorMessage!.Contains(validationContext.DisplayName));
        }
    }
}
