using System.Collections.Generic;
using ExcelDataReader;

namespace FileReader.Core
{
    public interface IExcelReadable
    {
         public void DeSerialize(IExcelDataReader reader, Dictionary<string, int> columns, int count){}
    }
}