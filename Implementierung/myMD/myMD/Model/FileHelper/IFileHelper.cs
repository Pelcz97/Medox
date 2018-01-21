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
        /// <returns>Der vollst�ndige Dateipfad</returns>
		string GetLocalFilePath(string filename);

        /// <summary>
        /// L�scht die gegebene Datei im lokalen Dateisystem
        /// </summary>
        /// <param name="filename">Die zu l�schende Datei</param>
        void DeleteFile(string filename);
    }
}