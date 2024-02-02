using Aiursoft.CSTools.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

    private class TestClass
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int PrivateProperty { get; private set; }
    }
}
