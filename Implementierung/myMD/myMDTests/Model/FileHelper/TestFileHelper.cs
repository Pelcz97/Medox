using myMD.Model.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMDTests.Model.FileHelper
{
    public class TestFileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);
            if(!File.Exists(path))
            {
                File.Create(path);
            }
            return path;
        }
    }
}
