using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void GetIcon_ValidExtension_ReturnsIcon()
        {
            // Arrange
            var fileName = "test.pdf";

            // Act
            var result = Mime.GetIcon(fileName);

            // Assert
            Assert.AreEqual("application-pdf.svg", result);
        }

        [TestMethod]
        public void GetIcon_UnknownExtension_ReturnsDefaultIcon()
        {
            // Arrange
            var fileName = "test.unknown_ext";

            // Act
            var result = Mime.GetIcon(fileName);

            // Assert
            Assert.AreEqual("unknown.svg", result);
        }


        [TestMethod]
        public void GetContentType_TS_ReturnsJavaScript()
        {
            Assert.AreEqual("text/javascript", Mime.GetContentType("ts"));
            Assert.AreEqual("text-x-typescript.svg", Mime.GetIcon("file.ts"));
        }

        [TestMethod]
        public void GetContentType_TSX_ReturnsJavaScript()
        {
            Assert.AreEqual("text/javascript", Mime.GetContentType("tsx"));
            Assert.AreEqual("text-x-typescript.svg", Mime.GetIcon("file.tsx"));
        }

        [TestMethod]
        public void GetContentType_JSX_ReturnsJavaScript()
        {
            Assert.AreEqual("text/javascript", Mime.GetContentType("jsx"));
            Assert.AreEqual("text-x-javascript.svg", Mime.GetIcon("file.jsx"));
        }

        [TestMethod]
        public void GetContentType_PSD_ReturnsPhotoshop()
        {
            Assert.AreEqual("image/vnd.adobe.photoshop", Mime.GetContentType("psd"));
            Assert.AreEqual("application-photoshop.svg", Mime.GetIcon("file.psd"));
        }

        [TestMethod]
        public void GetContentType_EXE_ReturnsMsDownload()
        {
            Assert.AreEqual("application/x-msdownload", Mime.GetContentType("exe"));
            Assert.AreEqual("application-x-msdownload.svg", Mime.GetIcon("file.exe"));
        }

        [TestMethod]
        public void GetContentType_LOG_ReturnsText()
        {
            Assert.AreEqual("text/plain", Mime.GetContentType("log"));
            Assert.AreEqual("text-x-generic.svg", Mime.GetIcon("file.log"));
        }

        [TestMethod]
        public void GetContentType_Vue_ReturnsText()
        {
            Assert.AreEqual("text/plain", Mime.GetContentType("vue"));
            Assert.AreEqual("text-html.svg", Mime.GetIcon("file.vue"));
        }

        [TestMethod]
        public void GetContentType_Env_ReturnsText()
        {
            Assert.AreEqual("text/plain", Mime.GetContentType("env"));
            Assert.AreEqual("application-x-theme.svg", Mime.GetIcon("file.env"));
        }

        [TestMethod]
        public void GetContentType_Conf_ReturnsText()
        {
            Assert.AreEqual("text/plain", Mime.GetContentType("conf"));
            Assert.AreEqual("text-x-script.svg", Mime.GetIcon("file.conf"));
        }

        [TestMethod]
        public void GetContentType_Ini_ReturnsText()
        {
            Assert.AreEqual("text/plain", Mime.GetContentType("ini"));
            Assert.AreEqual("text-x-script.svg", Mime.GetIcon("file.ini"));
        }

        [TestMethod]
        public void GetContentType_Map_ReturnsJson()
        {
            Assert.AreEqual("application/json", Mime.GetContentType("map"));
            Assert.AreEqual("application-json.svg", Mime.GetIcon("file.map"));
        }

        [TestMethod]
        public void GetContentType_DataFormats_ReturnsCorrectTypes()
        {
            // Google Apps
            Assert.AreEqual("application/vnd.google-apps.document", Mime.GetContentType("gdoc"));
            Assert.AreEqual("gddoc.svg", Mime.GetIcon("file.gdoc"));

            Assert.AreEqual("application/vnd.google-apps.spreadsheet", Mime.GetContentType("gsheet"));
            Assert.AreEqual("gdsheet.svg", Mime.GetIcon("file.gsheet"));

            Assert.AreEqual("application/vnd.google-apps.presentation", Mime.GetContentType("gslides"));
            Assert.AreEqual("gdslides.svg", Mime.GetIcon("file.gslides"));

            // VirtualBox
            Assert.AreEqual("application/x-virtualbox-ova", Mime.GetContentType("ova"));
            Assert.AreEqual("virtualbox-ova.svg", Mime.GetIcon("file.ova"));

            Assert.AreEqual("application/x-virtualbox-ovf", Mime.GetContentType("ovf"));
            Assert.AreEqual("virtualbox-ovf.svg", Mime.GetIcon("file.ovf"));

            Assert.AreEqual("application/x-virtualbox-vbox", Mime.GetContentType("vbox"));
            Assert.AreEqual("virtualbox-vbox.svg", Mime.GetIcon("file.vbox"));

            Assert.AreEqual("application/x-virtualbox-vdi", Mime.GetContentType("vdi"));
            Assert.AreEqual("virtualbox-vdi.svg", Mime.GetIcon("file.vdi"));

            Assert.AreEqual("application/x-virtualbox-vhd", Mime.GetContentType("vhd"));
            Assert.AreEqual("virtualbox-vhd.svg", Mime.GetIcon("file.vhd"));

            Assert.AreEqual("application/x-virtualbox-vmdk", Mime.GetContentType("vmdk"));
            Assert.AreEqual("virtualbox-vmdk.svg", Mime.GetIcon("file.vmdk"));
        }
    }
}
