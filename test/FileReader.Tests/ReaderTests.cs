using System;
using System.Collections.Generic;
using System.IO;
using FileReader.Core;
using Xunit;
namespace FileReader.Tests

{
    public class ReaderTests
    {
         [Fact]
        public void Constructor_filePathIsNullOrWhiteSpace_ThrowsArgumentException()
        {
            //Given
            DefineCustomEncoding();
            var reader = new Reader(null);
            //When
            //Then
            Assert.Throws<ArgumentException>(() => reader.Read<TypeDoesImplementIExcelReadable>());
        }

        private static void DefineCustomEncoding()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [Fact]
        public void Read_TypeImplementsIExcelReadable_ReturnsListofT()
        {
            //Arrange
            DefineCustomEncoding();
            var reader = new Reader();
            //Act
            var expected = reader.Read<TypeDoesImplementIExcelReadable>();
            //Assert
            Assert.IsType<List<TypeDoesImplementIExcelReadable>>(expected);
        }

        [Fact]
        public void Read_ValidFilePath_ReturnsExpectedNumberOfRows()
        {
        //Given
        DefineCustomEncoding();
        var reader = new Reader(filePath:  "C:/Users/ge080206/Downloads/E6 51426647-19_01_2021.xlsx");
        //When
        var expected =  (List<Subscription>) reader.Read<Subscription>();
        //Then
        Assert.Equal(171, expected.Count);
        }

        [Fact]
        public void Read_InvalidFilePath_ThrowsFileNotFoundException()
        {
        //Given
        DefineCustomEncoding();
        var reader = new Reader(filePath:  "C:/Users/ge080206/Downloads/E6 51426647-19_02_2021.xlsx");
        //When
        //Then
        Assert.Throws<FileNotFoundException>(() => reader.Read<TypeDoesImplementIExcelReadable>());
        }
    }
}