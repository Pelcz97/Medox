using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Diese Klasse enthält die Logik um Arztbriefe in ihr ursprüngliches Dateiformat zu parsen.
    /// </summary>
	public class LetterToOriginalFileParser
    {
        /// <summary>
        /// Parst den gegebenen Arztbrief in sein ursprüngliches Dateiformat.
        /// Da die usrprüngliche Datei gespeichert wird, muss nur ihr Pfad zurückgegeben werden.
        /// </summary>
        /// <param name="letter">Der zu parsende Arztbrief</param>
        /// <returns>Dateipfad zur geparsten Datei</returns>
		public string ParseLetter(IDoctorsLetter letter) => letter.ToDoctorsLetter().Filepath;
    }
}