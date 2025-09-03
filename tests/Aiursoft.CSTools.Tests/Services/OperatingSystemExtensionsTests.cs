using Aiursoft.CSTools.Services;

namespace Aiursoft.CSTools.Tests.Services;

[TestClass]
public class OperatingSystemExtensionsTests
{
    [TestMethod]
    public void TestGetOSNameAsync()
    {
        var osName = OperatingSystemExtensions.TryGetFullOsVersion();
        Console.WriteLine(osName);
        Assert.IsFalse(string.IsNullOrWhiteSpace(osName));
    }
}
