using myMD.Model.FileHelper;
using System;
using System.IO;

namespace myMDTests.Model.FileHelper
{
    public class TestFileHelper : IFileHelper
    {
        private static readonly string FOLDER = "Tests";

        public void DeleteFile(string filename)
        {
            File.Delete(GetLocalFilePath(filename));
        }

        public string GetLocalFilePath(string filename)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FOLDER, filename);
            if(!File.Exists(path))
            {
                File.Create(path);
            }
            return path;
        }
    }
}
