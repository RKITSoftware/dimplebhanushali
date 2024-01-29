using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BCLLibrary
{
    public class Class1 : StreamWriter
    {
        public Class1(Stream stream) : base(stream)
        {
        }
    }
}
