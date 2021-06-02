using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
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
            var reader = new Reader();
            //When
            //Then
            Assert.Throws<ArgumentException>(() => reader.Read<TypeDoesImplementIExcelReadable>(""));
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
        var mockFileSystem = new MockFileSystem();
        var mockInputFile = new MockFileData(@"Subscriber Name,Email Notification,Email Address,AAD Group,Subscription Level,Assigned,Activated,Expiration Date (UTC),Reference,Downloads,Country,Language,Subscription Status,Subscription Guid,Usage Status\n
        Adam Davies,adam.davies3@wales.nhs.uk,Adam.Davies3@wales.nhs.uk,False,Visual Studio Professional,24/09/2018 10:11:40,False,31/07/2021,,True,United Kingdom,English (United States),OverClaimed,88a177e8-dbf0-45ca-85c0-e3605c088a90,Used\n
        Adam Thomas Thomas,adam.thomas3@wales.nhs.uk,adam.thomas3@wales.nhs.uk,False,Visual Studio Professional,31/07/2015 00:00:00,False,31/07/2021,Software Development,True,United Kingdom,English (United States),OverClaimed,407db9f4-2b2f-432c-a468-019a092e6115,Used");
            
        
        mockFileSystem.AddFile(@"C:/temp/in.csv", mockInputFile);
        var reader = new Reader(mockFileSystem);
            //Act
            var expected = reader.Read<TypeDoesImplementIExcelReadable>(filePath: @"C:/temp/in.csv");
            //Assert
            Assert.IsType<List<TypeDoesImplementIExcelReadable>>(expected);
        }

        [Fact]
        public void Read_ValidFilePath_ReturnsExpectedNumberOfRows()
        {
        //Given
        DefineCustomEncoding();
        var mockFileSystem = new MockFileSystem();
        var mockInputFile = new MockFileData(@"Subscriber Name,Email Notification,Email Address,AAD Group,Subscription Level,Assigned,Activated,Expiration Date (UTC),Reference,Downloads,Country,Language,Subscription Status,Subscription Guid,Usage Status\n
        Adam Davies,adam.davies3@wales.nhs.uk,Adam.Davies3@wales.nhs.uk,False,Visual Studio Professional,24/09/2018 10:11:40,False,31/07/2021,,True,United Kingdom,English (United States),OverClaimed,88a177e8-dbf0-45ca-85c0-e3605c088a90,Used\n
        Adam Thomas Thomas,adam.thomas3@wales.nhs.uk,adam.thomas3@wales.nhs.uk,False,Visual Studio Professional,31/07/2015 00:00:00,False,31/07/2021,Software Development,True,United Kingdom,English (United States),OverClaimed,407db9f4-2b2f-432c-a468-019a092e6115,Used");
            
        
        mockFileSystem.AddFile(@"C:/temp/in.csv", mockInputFile);
        var reader = new Reader(mockFileSystem);
        //When
        var expected =  (List<Subscription>) reader.Read<Subscription>(filePath: @"C:/temp/in.csv");
        //Then
        Assert.Equal(2, expected.Count);
        }

        [Fact]
        public void Read_InvalidFilePath_ThrowsFileNotFoundException()
        {
        //Given
        DefineCustomEncoding();
        var mockFileSystem = new MockFileSystem();
        var mockInputFile = new MockFileData(@"Subscriber Name,Email Notification,Email Address,AAD Group,Subscription Level,Assigned,Activated,Expiration Date (UTC),Reference,Downloads,Country,Language,Subscription Status,Subscription Guid,Usage Status\n
        Adam Davies,adam.davies3@wales.nhs.uk,Adam.Davies3@wales.nhs.uk,False,Visual Studio Professional,24/09/2018 10:11:40,False,31/07/2021,,True,United Kingdom,English (United States),OverClaimed,88a177e8-dbf0-45ca-85c0-e3605c088a90,Used\n
        Adam Thomas Thomas,adam.thomas3@wales.nhs.uk,adam.thomas3@wales.nhs.uk,False,Visual Studio Professional,31/07/2015 00:00:00,False,31/07/2021,Software Development,True,United Kingdom,English (United States),OverClaimed,407db9f4-2b2f-432c-a468-019a092e6115,Used");
        mockFileSystem.AddFile(@"C:\temp\Valid.csv", mockInputFile);
        var reader = new Reader(mockFileSystem);
        //When
        //Then
        Assert.Throws<FileNotFoundException>(() => reader.Read<TypeDoesImplementIExcelReadable>(filePath: @"C:/temp/InValid.csv"));
        }
    }
}