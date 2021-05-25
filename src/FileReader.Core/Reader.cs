using System;
using System.Collections.Generic;
using System.IO;

namespace FileReader.Core
{
    public class Reader
    {
        private readonly string _filePath;
        public Reader()
        {
            var separator = Path.DirectorySeparatorChar;
            _filePath = $"C:{separator}Users{separator}ge080206{separator}Downloads{separator}E6 51426647-15_04_2021.xlsx";
        }

        public Reader(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<T> Read<T>() where T: IExcelReadable, new(){
            if (string.IsNullOrWhiteSpace(_filePath)) {
                throw new ArgumentException(
                                            message: "filePath name cannot be null or empty.", 
                                            paramName: nameof(_filePath)
                                            );
            }
            return new List<T>();
        }
    }
}