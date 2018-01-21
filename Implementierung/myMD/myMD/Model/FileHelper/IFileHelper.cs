namespace myMD.Model.FileHelper
{
    /// <summary>
    /// Schnittstelle zum Zugriff auf das lokale Dateisystem.
    /// Sollte plattformspezifisch implementiert werden.
    /// </summary>
	public interface IFileHelper
    {
        /// <summary>
        /// Erstellt die gegebene Datei im lokalen Dateisystem, falls sie noch nicht existiert
        /// </summary>
        /// <param name="filename">Dateipfad im lokalen Dateisystem</param>
        /// <returns>Der vollständige Dateipfad</returns>
		string GetLocalFilePath(string filename);

        /// <summary>
        /// Löscht die gegebene Datei im lokalen Dateisystem
        /// </summary>
        /// <param name="filename">Die zu löschende Datei</param>
        void DeleteFile(string filename);
    }
}