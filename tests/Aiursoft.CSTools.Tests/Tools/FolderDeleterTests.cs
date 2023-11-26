using Aiursoft.CSTools.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aiursoft.CSTools.Tests.Tools;

[TestClass]
public class FolderDeleterTests
{
    [TestMethod]
    public void TestDeleteFolder()
    {
        var testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(testDirectory);
        var testFile = Path.Combine(testDirectory, Guid.NewGuid().ToString());
        File.WriteAllText(testFile, "test");
        Assert.IsTrue(File.Exists(testFile));
        FolderDeleter.DeleteByForce(testDirectory);
        Assert.IsFalse(File.Exists(testFile));
        Assert.IsFalse(Directory.Exists(testDirectory));
    }
    
    [TestMethod]
    public void TestCleanFolder()
    {
        var testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(testDirectory);
        var testFile = Path.Combine(testDirectory, Guid.NewGuid().ToString());
        File.WriteAllText(testFile, "test");
        Assert.IsTrue(File.Exists(testFile));
        FolderDeleter.DeleteByForce(testDirectory, keepFolder: true);
        Assert.IsFalse(File.Exists(testFile));
        Assert.IsTrue(Directory.Exists(testDirectory));
    }
}