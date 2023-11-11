using Aiursoft.CSTools.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aiursoft.CSTools.Tests.Services;

[TestClass]
public class CommandServiceTests
{
    [TestMethod]
    public async Task TestPing()
    {
        var service = new CommandService();
        var (code, output, error) = await service.RunCommandAsync("ping", "bing.com -n 1", Path.GetTempPath());
        Assert.IsTrue(output.Contains("Pinging bing.com"));
        Assert.IsTrue(string.IsNullOrEmpty(error));
        Assert.AreEqual(0, code);
    }
}