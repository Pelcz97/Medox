using myMD.Model.FileHelper;
using System;
using System.IO;

namespace myMDTests.Model.FileHelper
{
    public class TestFileHelper : IFileHelper
    {
        private static readonly string FOLDER = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Resources");

        public string LastWrittenPath { get; private set; }

        public void DeleteFile(string filename)
        {
            File.Delete(GetLocalFilePath(filename));
        }

        public string GetLocalFilePath(string filename)
        {
            if (!Directory.Exists(FOLDER))
            {
                Directory.CreateDirectory(FOLDER);
            }
            string path = Path.Combine(FOLDER, filename);
            if(!File.Exists(path))
            {
                File.Create(path).Close();          
            }
            return path;
        }

        public string WriteLocalFileFromBytes(string format, byte[] data)
        {
            string path = Path.Combine(FOLDER, Guid.NewGuid().ToString() + format);
            File.WriteAllBytes(path, data);
            LastWrittenPath = path;
            return path;
        }

        public bool Exists(string filename)
        {
            return File.Exists(Path.Combine(FOLDER, filename));
        }
    }
}
