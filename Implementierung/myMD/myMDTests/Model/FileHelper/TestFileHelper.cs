using myMD.Model.FileHelper;
using System;
using System.IO;

namespace myMDTests.Model.FileHelper
{
    public class TestFileHelper : IFileHelper
    {
        private static readonly string FOLDER = "Resources";

        public void DeleteFile(string filename)
        {
            File.Delete(GetLocalFilePath(filename));
        }

        public string GetLocalFilePath(string filename)
        {
            string directory = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, FOLDER);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string path = Path.Combine(directory, filename);
            if(!File.Exists(path))
            {
                File.Create(path);
            }
            return path;
        }
    }
}
