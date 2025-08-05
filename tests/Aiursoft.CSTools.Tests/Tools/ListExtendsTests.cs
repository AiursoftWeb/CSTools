using Aiursoft.CSTools.Tools;

namespace Aiursoft.CSTools.Tests.Tools
{
    [TestClass]
    public class ListExtendsTests
    {
        [TestMethod]
        public void Shuffle_ShouldShuffleList()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            // Act
            list.Shuffle();

            // Assert
            CollectionAssert.AreNotEqual(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 }, list);
        }
        
        [TestMethod]
        public void Shuffle_ShouldNotChangeListSize()
        {
            // Arrange
            var list = new List<string> { "a", "b", "c", "d", "e" };
            var originalCount = list.Count;

            // Act
            list.Shuffle();

            // Assert
            Assert.AreEqual(originalCount, list.Count);
        }
        
        [TestMethod]
        public void Shuffle_ShouldNotChangeListElements()
        {
            // Arrange
            var list = new List<string?> { "a", "b", "c", null, "e" };
            var originalList = new List<string?>(list);

            // Act
            list.Shuffle();

            // Assert
            CollectionAssert.AreEquivalent(originalList, list);
        }
    }
}