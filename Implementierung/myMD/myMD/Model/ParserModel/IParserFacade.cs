using System;
using System.Threading.Tasks;
using myMD.Model.DatabaseModel;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Diese Schnittstelle ist die Haupteinstiegsstelle für andere Pakete in das ParserModel Paket.
    /// Hier können Informationen aus Dateien in eine Datenbank eingetragen werden oder Dateien aus Informationen aus der Datenbank erstellt werden.
    /// </summary>
	public interface IParserFacade
    {
        /// <summary>
        /// Parse die Informationen in der gegebenen Datei in die gegebene Datenbank.
        /// </summary>
        /// <param name="filename">Die zu parsende Datei</param>
        /// <param name="db">Die Datenbank in die die geparsten Informationen eingetragen werden sollen</param>
        /// <exception cref="System.NotSupportedException">Werfe, wenn das Dateiformat der gegebenen Datei nicht unterstützt wird</exception>
		void ParseFileToDatabase(string filename, IEntityDatabase db);

        /// <summary>
        /// Parse den gegebenen Arztbrief in sein ursprüngliches Dateiformat
        /// </summary>
        /// <param name="letter">Der zu parsende Arztbrief</param>
        /// <returns>Pfad zur geparsten Datei</returns>
		string ParseLetterToOriginalFile(IDoctorsLetter letter);

        Task<string> ParseImageToDatabase(byte[] image);

        void CreateDoctorsLetter(string DoctorsName, string DoctorsField, DateTime LetterDate, string Diagnosis, IEntityDatabase db);
    }
}