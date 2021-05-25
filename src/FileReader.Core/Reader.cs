using System;
using System.Collections.Generic;

namespace FileReader.Core
{
    public class Reader
    {
        public Reader()
        {
        }

        public IEnumerable<T> Read<T>() where T: IExcelReadable, new(){
            return new List<T>();
        }
    }
}