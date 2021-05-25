using System;
using System.Collections.Generic;
using System.IO;
using ExcelDataReader;

namespace FileReader.Core
{
    public class Reader
    {
        private readonly string _filePath;
        public Reader()
        {
            var separator = Path.DirectorySeparatorChar;
            _filePath = $"C:{separator}Users{separator}ge080206{separator}Downloads{separator}E6 51426647-19_01_2021.xlsx";
        }

        public Reader(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<T> Read<T>() where T : IExcelReadable, new()
        {
            List<T> objectList = new List<T>();
            if (string.IsNullOrWhiteSpace(_filePath)) throw new ArgumentException(message: "filePath name cannot be null or empty.", paramName: nameof(_filePath));
            using var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var _columns = BuildColumns(reader);

            int count = 0;
            while (reader.Read())
            {
                var obj = new T();
                obj.DeSerialize(reader, _columns, count);
                objectList.Add(obj);
                count++;
            }
            return objectList;
        }
           private Columns<string, int> _columns;
            private Columns<string, int> BuildColumns(IExcelDataReader reader)
        {
            reader.Read();
            _columns = new Columns<string, int>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                _columns.Add(reader.GetString(i), i);
            }
            return _columns;
        }
    }
}