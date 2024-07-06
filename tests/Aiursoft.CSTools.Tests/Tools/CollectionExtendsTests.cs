using Aiursoft.CSTools.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aiursoft.CSTools.Tests.Tools;

[TestClass]
public class CollectionExtendsTests
{
    [TestMethod]
    public async Task TryFindFirstTestAsync()
    {
        var collection = new List<int> { 1, 2, 3, 4, 5 };
        await collection.TryFindFirst(
            t => t == 3,
            async t =>
            {
                await Task.Delay(100);
                Assert.AreEqual(3, t);
            },
            () =>
            {
                Assert.Fail();
                return Task.CompletedTask;
            });
    }
    
    [TestMethod]
    public async Task TryFindNotFoundTestAsync()
    {
        var collection = new List<int> { 1, 2, 3, 4, 5 };
        await collection.TryFindFirst(
            t => t == 6,
            _ =>
            {
                Assert.Fail();
                return Task.CompletedTask;
            },
            async () =>
            {
                await Task.Delay(100);
                Assert.IsTrue(true);
            });
    }
    
    [TestMethod]
    public void TryFindFirstTestSync()
    {
        var collection = new List<int> { 1, 2, 3, 4, 5 };
        collection.TryFindFirst(
            t => t == 3,
            t =>
            {
                Assert.AreEqual(3, t);
            },
            () =>
            {
                Assert.Fail();
            });
    }
    
    [TestMethod]
    public void TryFindNotFoundTestSync()
    {
        var collection = new List<int> { 1, 2, 3, 4, 5 };
        collection.TryFindFirst(
            t => t == 6,
            _ =>
            {
                Assert.Fail();
            },
            () =>
            {
                Assert.IsTrue(true);
            });
    }
}