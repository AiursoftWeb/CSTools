using Aiursoft.CSTools.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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