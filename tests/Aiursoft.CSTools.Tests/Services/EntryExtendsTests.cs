using Aiursoft.CSTools.Tools;

namespace Aiursoft.CSTools.Tests.Services;

[TestClass]
public class EntryExtendsTests
{
    [TestMethod]
    public void TestIsInUt()
    {
        Assert.IsTrue(EntryExtends.IsInUnitTests());
    }
    
    [TestMethod]
    public void TestIsNotAProgram()
    {
        Assert.IsFalse(EntryExtends.IsProgramEntry());
        Assert.IsFalse(EntryExtends.IsProgramEntry());
    }
    
    [TestMethod]
    public void TestIsNotEntityFramework()
    {
        Assert.IsFalse(EntryExtends.IsInEntityFramework());
    }
}