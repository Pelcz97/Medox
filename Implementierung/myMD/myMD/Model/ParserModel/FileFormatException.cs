using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMD.Model.ParserModel
{
    public class FileFormatException : FormatException
    {
        public FileFormatException(string message, Exception innerException) : base(message, innerException) { }

        public FileFormatException(string message) : base(message) { }
    }
}
