namespace myMD.Model.FileHelper
{
    /// <summary>
    /// Schnittstelle zum Zugriff auf das lokale Dateisystem.
    /// Sollte plattformspezifisch implementiert werden.
    /// </summary>
	public interface IFileHelper
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		string GetLocalFilePath(string filename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        void DeleteFile(string filename);
	}

}

