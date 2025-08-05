using System.ComponentModel.DataAnnotations;
using Aiursoft.CSTools.Attributes;

namespace Aiursoft.CSTools.Tests.Attributes;

[TestClass]
public class IsGuidOrEmptyTests
{
    [TestMethod]
    public void IsValid_ValidGuid_ReturnsTrue()
    {
        // Arrange
        var attribute = new IsGuidOrEmpty();

        // Act
        var result = attribute.IsValid("3F2504E0-4F89-11D3-9A0C-0305E82C3301");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValid_EmptyString_ReturnsTrue()
    {
        // Arrange
        var attribute = new IsGuidOrEmpty();

        // Act
        var result = attribute.IsValid(string.Empty);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValid_Null_ReturnsFalse()
    {
        // Arrange
        var attribute = new IsGuidOrEmpty();

        // Act
        var result = attribute.IsValid(null);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_InvalidGuid_ReturnsFalse()
    {
        // Arrange
        var attribute = new IsGuidOrEmpty();

        // Act
        var result = attribute.IsValid("InvalidGuid");

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsValid_ErrorMessageContainsDisplayName()
    {
        // Arrange
        var attribute = new IsGuidOrEmpty();
        var value = "InvalidGuid";
        var validationContext = new ValidationContext(value) { DisplayName = "TestValue" };

        // Act
        var result = attribute.GetValidationResult(value, validationContext);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.ErrorMessage != null && result.ErrorMessage.Contains(validationContext.DisplayName));
    }
}
