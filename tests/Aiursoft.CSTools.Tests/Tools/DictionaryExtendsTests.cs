using Aiursoft.CSTools.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aiursoft.CSTools.Tests.Tools;

[TestClass]
public class DictionaryExtendsTests
{
    [TestMethod]
    public void TestGetOrAdd()
    {
        var dict = new Dictionary<string, int>();
        var value = dict.GetOrAdd("test", () => 1);
        Assert.AreEqual(1, value);
        Assert.AreEqual(1, dict["test"]);
    }
    
    [TestMethod]
    public void TestGetOrAddMultiple()
    {
        var dict = new Dictionary<string, int>();
        var value = dict.GetOrAdd("test", () => 1);
        Assert.AreEqual(1, value);
        Assert.AreEqual(1, dict["test"]);
        var value2 = dict.GetOrAdd("test", () => 2);
        Assert.AreEqual(1, value2);
        Assert.AreEqual(1, dict["test"]);
    }
    
    [TestMethod]
    public async Task TestCopyFilesRecursively()
    {
        // Prepare
        var sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets");
        if (!Directory.Exists(sourcePath))
        {
            Directory.CreateDirectory(sourcePath);
        }
        if (!Directory.Exists(Path.Combine(sourcePath, "testDirectory")))
        {
            Directory.CreateDirectory(Path.Combine(sourcePath, "testDirectory"));
        }
        File.Create(Path.Combine(sourcePath, "test1.txt")).Close();
        await File.WriteAllTextAsync(Path.Combine(sourcePath, "test1.txt"), "test");
        File.Create(Path.Combine(sourcePath, "testDirectory", "test2.txt")).Close();
        await File.WriteAllTextAsync(Path.Combine(sourcePath, "testDirectory", "test2.txt"), "test2");
        var targetPath = Path.Combine(Path.GetTempPath(), $"CSTools-UT-{Guid.NewGuid()}");
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        
        // Test
        sourcePath.CopyFilesRecursively(targetPath);
        
        // Assert
        Assert.IsTrue(Directory.Exists(targetPath));
        Assert.IsTrue(Directory.GetFiles(targetPath, "*", SearchOption.AllDirectories).Length > 0);
        Assert.IsTrue(File.Exists(Path.Combine(targetPath, "test1.txt")));
        Assert.AreEqual("test", await File.ReadAllTextAsync(Path.Combine(targetPath, "test1.txt")));
        Assert.IsTrue(File.Exists(Path.Combine(targetPath, "testDirectory", "test2.txt")));
        Assert.AreEqual("test2", await File.ReadAllTextAsync(Path.Combine(targetPath, "testDirectory", "test2.txt")));
        
        // Clean
        Directory.Delete(sourcePath, true);
        Directory.Delete(targetPath, true);
    }
}