using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(myMD.Model.FileHelper.iOS.FileHelper))]
namespace myMD.Model.FileHelper.iOS
{
    /// <summary>
    /// Implementierung der IFileHelper Schnittstelle für iOS-Anwendungen
    /// </summary>
    /// <see>myMD.Model.FileHelper.IFileHelper</see>
    [Preserve(AllMembers = true)]
    public class FileHelper : IFileHelper
    {
        private static readonly string PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library");
        /// <see>myMD.Model.FileHelper.IFileHelper#DeleteFile(string)</see>
        public void DeleteFile(string filename)
        {
            File.Delete(GetLocalFilePath(filename));
        }

        /// <see>myMD.Model.FileHelper.IFileHelper#GetLocalFilePath(string)</see>
        public string GetLocalFilePath(string filename)
		{
            string directory = Path.Combine(PATH, filename);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string path = Path.Combine(directory, filename);
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            return path;
        }

        public string WriteLocalFileFromBytes(string format, byte[] data)
        {
            string path = Path.Combine(PATH, Guid.NewGuid().ToString() + format);
            File.WriteAllBytes(path, data);
            return path;
        }

    }

}

