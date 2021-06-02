using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using ExcelDataReader;

namespace FileReader.Core
{
    public class Reader
    {
        private readonly IFileSystem _filesystem;

        public Reader() : this(new FileSystem())
        {
            var separator = Path.DirectorySeparatorChar;
        }

        public Reader(IFileSystem filesystem)
        {
            _filesystem = filesystem;

        }


        public IEnumerable<T> Read<T>(string filePath) where T : IExcelReadable, new()
        {
            List<T> objectList = new List<T>();
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException(message: "filePath name cannot be null or empty.", paramName: nameof(filePath));
            using var stream = _filesystem.File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateCsvReader(stream);

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