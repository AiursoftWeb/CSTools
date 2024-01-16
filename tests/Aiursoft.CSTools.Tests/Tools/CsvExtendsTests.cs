using Aiursoft.CSTools.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aiursoft.CSTools.Tools;

namespace Aiursoft.CSTools.Tests.Tools
{
    [TestClass]
    public class CsvExtendsTests
    {
        [TestMethod]
        public void ToCsv_Should_Return_Correct_Byte_Array()
        {
            // Arrange
            var items = new List<TestItem>
            {
                new() { Id = 1, Name = "Item 1", Price = 10.99 },
                new() { Id = 2, Name = "Item 2", Price = 19.99 },
                new() { Id = 3, Name = "Item 3", Price = 5.99 }
            };

            // Act
            var result = items.ToCsv();
            
            // result is UTF-8 with BOM
            // Decode with UTF-8 with bom
            var resultString = result.Utf8WithBomToString();
            Assert.AreEqual(@"""Id"",""Name""
""1"",""Item 1""
""2"",""Item 2""
""3"",""Item 3""
", resultString);
        }
    }

    // TestItem class used for testing
    public class TestItem
    {
        [CsvProperty(name: "Id")]
        public int? Id { get; set; }
        [CsvProperty(name: "Name")]
        public string? Name { get; set; }
        public double? Price { get; set; }
    }
}