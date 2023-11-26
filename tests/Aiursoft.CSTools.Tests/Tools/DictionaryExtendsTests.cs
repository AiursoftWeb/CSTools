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
}