using Aiursoft.CSTools.Models;

namespace Aiursoft.CSTools.Tests.Models
{
    [TestClass]
    public class MimeTests
    {
        [TestMethod]
        public void CanHandle_ValidFileName_ReturnsTrue()
        {
            // Arrange
            var fileName = "test.jpg";

            // Act
            var result = Mime.CanHandle(fileName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanHandle_InvalidFileName_ReturnsFalse()
        {
            // Arrange
            var fileName = "test.xyz";

            // Act
            var result = Mime.CanHandle(fileName);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsVideo_VideoFileName_ReturnsTrue()
        {
            // Arrange
            var fileName = "video.mp4";

            // Act
            var result = Mime.IsVideo(fileName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsVideo_NonVideoFileName_ReturnsFalse()
        {
            // Arrange
            var fileName = "image.jpg";

            // Act
            var result = Mime.IsVideo(fileName);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetContentType_ExistingExtension_ReturnsContentType()
        {
            // Arrange
            var extension = "jpg";

            // Act
            var result = Mime.GetContentType(extension);

            // Assert
            Assert.AreEqual("image/jpeg", result);
        }

        [TestMethod]
        public void GetContentType_NonExistingExtension_ReturnsDefaultContentType()
        {
            // Arrange
            var extension = "xyz";

            // Act
            var result = Mime.GetContentType(extension);

            // Assert
            Assert.AreEqual("application/octet-stream", result);
        }
    }
}
