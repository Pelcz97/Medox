using Xamarin.Forms;
using System;
using System.IO;

[assembly: Dependency(typeof(myMD.Model.FileHelper.Droid.FileHelper))]
namespace myMD.Model.FileHelper.Droid
{
    /// <summary>
    /// Implementierung der IFileHelper Schnittstelle für iOS-Anwendungen
    /// </summary>
    /// <see>myMD.Model.FileHelper.IFileHelper</see>
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
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            return path;
        }

	}

}

