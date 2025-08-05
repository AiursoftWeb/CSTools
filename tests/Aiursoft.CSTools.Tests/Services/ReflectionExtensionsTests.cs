using Aiursoft.CSTools.Services;

namespace Aiursoft.CSTools.Tests.Services;

[TestClass]
public class ReflectionExtensionsTests
{
    [TestMethod]
    public void SetPrivatePropertyValue_WhenCalled_ShouldSetPrivateProperty()
    {
        // Arrange
        var obj = new TestClass();
        const string propName = "PrivateProperty";
        const int val = 10;

        // Act
        obj.SetPrivatePropertyValue(propName, val);

        // Assert
        Assert.AreEqual(val, obj.PrivateProperty);
    }

    [TestMethod]
    public void SetPrivateNonExistentPropertyValue_WhenCalled_ShouldThrowException()
    {
        // Arrange
        var obj = new TestClass();
        const string propName = "NonExistentProperty";
        const int val = 10;

        // Act and Assert
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => obj.SetPrivatePropertyValue(propName, val));
    }

    private class TestClass
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int PrivateProperty { get; private set; }
    }
}
