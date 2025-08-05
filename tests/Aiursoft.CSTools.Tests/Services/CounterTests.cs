using Aiursoft.CSTools.Services;

namespace Aiursoft.CSTools.Tests.Services
{
    [TestClass]
    public class CounterTests
    {
        [TestMethod]
        public void GetCurrent_ShouldReturnInitialValue()
        {
            // Arrange
            var counter = new Counter();

            // Act
            var result = counter.GetCurrent;

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetUniqueNo_ShouldReturnIncrementedValue()
        {
            // Arrange
            var counter = new Counter();

            // Act
            var result1 = counter.GetUniqueNo();
            var result2 = counter.GetUniqueNo();

            // Assert
            Assert.AreEqual(1, result1);
            Assert.AreEqual(2, result2);
        }
    }
}