using System;
using System.Diagnostics;
using System.IO;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(myMD.Model.FileHelper.iOS.FileHelper))]
namespace myMD.Model.FileHelper.iOS
{
    /// <summary>
    /// Implementierung der IFileHelper Schnittstelle für iOS-Anwendungen
    /// </summary>
    /// <see>myMD.Model.FileHelper.IFileHelper</see>
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class FileHelper : IFileHelper
    {
        private static readonly string DOCS = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string PATH = Path.Combine(DOCS, "..", "Library");
        /// <see>myMD.Model.FileHelper.IFileHelper#DeleteFile(string)</see>
        public void DeleteFile(string filename)
        {
            File.Delete(GetLocalFilePath(filename));
        }

        /// <see>myMD.Model.FileHelper.IFileHelper#GetLocalFilePath(string)</see>
        public string GetLocalFilePath(string filename)
		{
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            string path = Path.Combine(PATH, filename);
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            return path;
        }

        public string WriteLocalFileFromString(string format, string data)
        {
            string filename = Guid.NewGuid().ToString() + format;
            string path = GetLocalFilePath(filename);
            File.WriteAllText(path, data);
            return filename;
        }

    }

}

