using System.Runtime.InteropServices;
using Aiursoft.CSTools.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aiursoft.CSTools.Tests.Services;

[TestClass]
public class CommandServiceTests
{
    [TestMethod]
    public async Task TestPing()
    {
        // If Windows, -n 1
        // If Linux, -c 1
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var service = new CommandService();
            var (code, output, error) = await service.RunCommandAsync("ping", "bing.com -n 1", Path.GetTempPath());
            Assert.IsTrue(output.Contains("Pinging bing.com"));
            Assert.IsTrue(string.IsNullOrEmpty(error));
            Assert.AreEqual(0, code);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var service = new CommandService();
            var (code, output, error) = await service.RunCommandAsync("ping", "bing.com -c 1", Path.GetTempPath());
            Assert.IsTrue(output.Contains("Pinging bing.com"));
            Assert.IsTrue(string.IsNullOrEmpty(error));
            Assert.AreEqual(0, code);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var service = new CommandService();
            var (code, output, error) = await service.RunCommandAsync("ping", "bing.com -c 1", Path.GetTempPath());
            Assert.IsTrue(output.Contains("Pinging bing.com"));
            Assert.IsTrue(string.IsNullOrEmpty(error));
            Assert.AreEqual(0, code);
        }
        else
        {
            Assert.Fail("Unknown OS!");
        }
    }
}