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
        /// <see>myMD.Model.FileHelper.IFileHelper#DeleteFile(string)</see>
        public void DeleteFile(string filename)
        {
            File.Delete(GetLocalFilePath(filename));
        }

        /// <see>myMD.Model.FileHelper.IFileHelper#GetLocalFilePath(string)</see>
        public string GetLocalFilePath(string filename)
		{
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Library");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string path = Path.Combine(directory, filename);
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            return path;
        }

	}

}

