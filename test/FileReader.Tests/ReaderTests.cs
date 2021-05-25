using System;
using System.Collections.Generic;
using FileReader.Core;
using Xunit;
namespace FileReader.Tests

{
    public class ReaderTests
    {
        [Fact]
        public void Read_TypeImplementsIExcelReadable_ReturnsListofT()
        {
            //Arrange
            var reader = new Reader();
            //Act
            var expected = reader.Read<FileReader.Core.TypeDoesImplementIExcelReadable>();
            //Assert
            Assert.IsType<List<TypeDoesImplementIExcelReadable>>(expected);
        }
        [Fact]
        public void Read_filePathIsNullOrWhiteSpace_ThrowsArgumentException()
        {
        //Given
        var reader = new Reader(null);
                      
        //When
        
        //Then
        Assert.Throws<ArgumentException>(()=>reader.Read<FileReader.Core.TypeDoesImplementIExcelReadable>());
        }
    }
}