using System.ComponentModel;
using System.Runtime.InteropServices;
using Aiursoft.CSTools.Services;
using Aiursoft.CSTools.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aiursoft.CSTools.Tests.Services;

[TestClass]
public class CommandServiceTests
{
    private readonly string _testCommand =
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "-n 2 baidu.com" : "-c 2 baidu.com";
    
    [TestMethod]
    public async Task TestPing()
    {
        var service = new CommandService();
        var (_, output, error) = await service.RunCommandAsync("ping", _testCommand, Environment.CurrentDirectory);
        Assert.IsTrue(output.Contains("baidu.com"));
        Assert.IsTrue(string.IsNullOrEmpty(error));
    }
    
    [TestMethod]
    public async Task TestProgramNotExist()
    {
        var service = new CommandService();
        await Assert.ThrowsExceptionAsync<Win32Exception>(async () =>
        {
            await service.RunCommandAsync("notexist", string.Empty, Environment.CurrentDirectory);
        });
    }
    
    [TestMethod]
    public async Task TestProgramTimeout()
    {
        var service = new CommandService();
        await Assert.ThrowsExceptionAsync<TimeoutException>(async () =>
        {
            await service.RunCommandAsync("ping", _testCommand, Environment.CurrentDirectory, TimeSpan.FromMilliseconds(1));
        });
    }
    
    [TestMethod]
    public async Task TestProgramError()
    {
        var service = new CommandService();
        var (code, output, error) = await service.RunCommandAsync("ping", "-n 2 notexist", Environment.CurrentDirectory);
        Assert.IsTrue(output.ToLower().Contains("ping") || error.ToLower().Contains("ping")); 
        Assert.IsTrue(code > 0);
    }
    
    [TestMethod]
    public async Task TestLargeOutput()
    {
        var service = new CommandService();
        var testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        _ = await service.RunCommandAsync("git", "clone https://github.com/ediwang/moonglade.git --bare --filter=tree:0 .", testDirectory);
        var (code, output, error) = await service.RunCommandAsync("git", "--no-pager log --pretty=format:\"%H\" --max-count=2000", testDirectory);
        Assert.AreEqual(0, code);
        Assert.IsTrue(string.IsNullOrEmpty(error));
        // Total Lines:
        Assert.AreEqual(2000, output.Split('\n').Length);
        
        // Clean
        FolderDeleter.DeleteByForce(testDirectory);
    }
}