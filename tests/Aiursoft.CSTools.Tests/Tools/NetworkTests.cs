using Aiursoft.CSTools.Tools;

namespace Aiursoft.CSTools.Tests.Tools
{
    [TestClass]
    public class NetworkTests
    {
        [TestMethod]
        public void GetAvailablePort_ReturnsValidPort()
        {
            // Arrange
            
            // Act
            var port = Network.GetAvailablePort();
            
            // Assert
            Assert.IsTrue(port is > 0 and < 65534);
        }
    }
}