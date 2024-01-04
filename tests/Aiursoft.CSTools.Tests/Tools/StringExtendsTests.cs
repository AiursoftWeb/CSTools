using Aiursoft.CSTools.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aiursoft.CSTools.Tests.Tools
{
    [TestClass]
    public class StringExtendsTests
    {
        [TestMethod]
        public void BytesToBase64_ShouldConvertBytesToBase64String()
        {
            // Arrange
            byte[] input = { 72, 101, 108, 108, 111 };
            string expected = "SGVsbG8=";

            // Act
            string result = input.BytesToBase64();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Base64ToBytes_ShouldConvertBase64StringToBytes()
        {
            // Arrange
            string input = "SGVsbG8=";
            byte[] expected = { 72, 101, 108, 108, 111 };

            // Act
            byte[] result = input.Base64ToBytes();

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StringToBytes_ShouldConvertStringToBytes()
        {
            // Arrange
            string input = "Hello";
            byte[] expected = { 72, 101, 108, 108, 111 };

            // Act
            byte[] result = input.StringToBytes();

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void BytesToString_ShouldConvertBytesToString()
        {
            // Arrange
            byte[] input = { 72, 101, 108, 108, 111 };
            string expected = "Hello";

            // Act
            string result = input.BytesToString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StringToBase64_ShouldConvertStringToBase64String()
        {
            // Arrange
            string input = "Hello";
            string expected = "SGVsbG8=";

            // Act
            string result = input.StringToBase64();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Base64ToString_ShouldConvertBase64StringToString()
        {
            // Arrange
            string input = "SGVsbG8=";
            string expected = "Hello";

            // Act
            string result = input.Base64ToString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToUtf8WithDom_ShouldAddUtf8BomToContent()
        {
            // Arrange
            string content = "Hello";
            byte[] expected = { 0xEF, 0xBB, 0xBF, 72, 101, 108, 108, 111 };

            // Act
            byte[] result = content.StringToUtf8WithBom();

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetMd5_ShouldReturnMd5HashOfString()
        {
            // Arrange
            string sourceString = "Hello";
            string expected = "8b1a9953c4611296a827abf8c47804d7";

            // Act
            string result = sourceString.GetMd5();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetMd5_ShouldReturnMd5HashOfBytes()
        {
            // Arrange
            byte[] data = { 72, 101, 108, 108, 111 };
            string expected = "8B1A9953C4611296A827ABF8C47804D7";

            // Act
            string result = data.GetMd5();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SafeSubstring_ShouldReturnSubstringUpToMaxLength()
        {
            // Arrange
            string source = "Hello World";
            int maxLength = 5;
            string expected = "He...";

            // Act
            string result = source.SafeSubstring(maxLength);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IsInFollowingExtension_ShouldReturnTrueIfFilenameHasMatchingExtension()
        {
            // Arrange
            string filename = "image.jpg";
            string[] extensions = { "jpg", "png", "bmp", "jpeg" };

            // Act
            bool result = filename.IsInFollowingExtension(extensions);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsInFollowingExtension_ShouldReturnFalseIfFilenameDoesNotHaveMatchingExtension()
        {
            // Arrange
            string filename = "document.pdf";
            string[] extensions = { "jpg", "png", "bmp", "jpeg" };

            // Act
            bool result = filename.IsInFollowingExtension(extensions);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsStaticImage_ShouldReturnTrueIfFilenameHasStaticImageExtension()
        {
            // Arrange
            string filename = "image.jpg";

            // Act
            bool result = filename.IsStaticImage();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsStaticImage_ShouldReturnFalseIfFilenameDoesNotHaveStaticImageExtension()
        {
            // Arrange
            string filename = "document.pdf";

            // Act
            bool result = filename.IsStaticImage();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveTags_ShouldRemoveHtmlTagsFromContent()
        {
            // Arrange
            string content = "<p>Hello <b>World</b></p>";
            string expected = "Hello World";

            // Act
            string result = content.RemoveTags();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RandomString_ShouldReturnRandomStringWithGivenLength()
        {
            // Arrange
            int count = 10;

            // Act
            string result = StringExtends.RandomString(count);

            // Assert
            Assert.AreEqual(count, result.Length);
        }

        [TestMethod]
        public void EncodePath_ShouldEncodePathAndReplaceSlash()
        {
            // Arrange
            string input = "folder/f@ile.txt";
            string expected = "folder/f%40ile.txt";

            // Act
            string result = input.EncodePath();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToUrlEncoded_ShouldEncodeStringToUrlEncodedFormat()
        {
            // Arrange
            string input = "Hello World";
            string expected = "Hello%20World";

            // Act
            string result = input.ToUrlEncoded();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void AppendPath_ShouldAppendFolderToRootPath()
        {
            // Arrange
            string root = "root";
            string folder = "folder";
            string expected = "root/folder";

            // Act
            string result = root.AppendPath(folder);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DetachPath_ShouldRemoveLastFolderFromPath()
        {
            // Arrange
            string path = "root/folder/subfolder";
            string expected = "root/folder";

            // Act
            string? result = path.DetachPath();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DetachPath_ShouldReturnNullIfPathDoesNotContainFolders()
        {
            // Arrange
            string path = "root";

            // Act
            string? result = path.DetachPath();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SplitInParts_ShouldSplitInputStringIntoPartsOfGivenLength()
        {
            // Arrange
            string input = "Hello World";
            int partLength = 5;
            // ReSharper disable once StringLiteralTypo
            string[] expected = { "Hello", " Worl", "d" };

            // Act
            var result = input.SplitInParts(partLength).ToList();

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void HumanReadableSize_ShouldReturnHumanReadableSizeString()
        {
            // Arrange
            long size = 1024 * 1024 * 2 + 1024 * 100 + 1024 * 30 + 1024 * 4;
            string expected = "2.13 MB";

            // Act
            string result = size.HumanReadableSize();

            // Assert
            Assert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void IsValidJson_WhenValidJsonObject_ReturnsTrue()
        {
            // Arrange
            string jsonString = "{\"name\":\"John\",\"age\":30,\"city\":\"New York\"}";

            // Act
            bool isValid = jsonString.IsValidJson();

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidJson_WhenValidJsonArray_ReturnsTrue()
        {
            // Arrange
            string jsonString = "[\"apple\",\"banana\",\"cherry\"]";

            // Act
            bool isValid = jsonString.IsValidJson();

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidJson_WhenValidJsonString_ReturnsTrue()
        {
            // Arrange
            string jsonString = "\"Hello, World!\"";

            // Act
            bool isValid = jsonString.IsValidJson();

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidJson_WhenEmptyString_ReturnsFalse()
        {
            // Arrange
            string jsonString = "";

            // Act
            bool isValid = jsonString.IsValidJson();

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidJson_WhenWhitespaceString_ReturnsFalse()
        {
            // Arrange
            string jsonString = "     ";

            // Act
            bool isValid = jsonString.IsValidJson();

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidJson_WhenInvalidJson_ReturnsFalse()
        {
            // Arrange
            string jsonString = "invalid json";

            // Act
            bool isValid = jsonString.IsValidJson();

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}