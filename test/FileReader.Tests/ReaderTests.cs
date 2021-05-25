using System.Collections.Generic;
using FileReader.Core;
using Xunit;
namespace FileReader.Tests

{
    public class ReaderTests
    {
        [Fact]
        public void Read_WithValidFilePathandObjectTypeT_ReturnsListofT()
        {
            //Arrange
            var reader = new Reader();
            //Act
            var expected = reader.Read<FileReader.Core.Subscription>();
            //Assert
            Assert.IsType<List<Subscription>>(expected);
        }
    }
}