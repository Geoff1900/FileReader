using System.Collections.Generic;

namespace FileReader.Tests
{
    public class TypeDoesImplementIExcelReadable : FileReader.Core.IExcelReadable
    {
        public void DeSerialize()
        {
            throw new System.NotImplementedException();
        }
    }
}