using Xamarin.Forms;
using System;
using System.IO;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(myMD.Model.FileHelper.Droid.FileHelper))]
namespace myMD.Model.FileHelper.Droid
{
    /// <summary>
    /// Implementierung der IFileHelper Schnittstelle für iOS-Anwendungen
    /// </summary>
    /// <see>myMD.Model.FileHelper.IFileHelper</see>
    [Preserve(AllMembers = true)]
    public class FileHelper : IFileHelper
	{
        private static readonly string PATH = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        /// <see>myMD.Model.FileHelper.IFileHelper#DeleteFile(string)</see>
        public void DeleteFile(string filename)
        {
            File.Delete(GetLocalFilePath(filename));
        }

        /// <see>myMD.Model.FileHelper.IFileHelper#GetLocalFilePath(string)</see>
        public string GetLocalFilePath(string filename)
		{
            string path = Path.Combine(PATH, filename);
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            return path;
        }

        public string WriteLocalFileFromBytes(string format, string data)
        {
            string path = Path.Combine(PATH, Guid.NewGuid().ToString() + format);
            File.WriteAllText(path, data);
            return path;
        }
    }

}

