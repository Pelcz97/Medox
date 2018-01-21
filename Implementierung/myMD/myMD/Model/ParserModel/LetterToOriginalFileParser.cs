using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Diese Klasse enth�lt die Logik um Arztbriefe in ihr urspr�ngliches Dateiformat zu parsen.
    /// </summary>
	public class LetterToOriginalFileParser
    {
        /// <summary>
        /// Parst den gegebenen Arztbrief in sein urspr�ngliches Dateiformat.
        /// Da die usrpr�ngliche Datei gespeichert wird, muss nur ihr Pfad zur�ckgegeben werden.
        /// </summary>
        /// <param name="letter">Der zu parsende Arztbrief</param>
        /// <returns>Dateipfad zur geparsten Datei</returns>
		public string ParseLetter(IDoctorsLetter letter) => letter.ToDoctorsLetter().Filepath;
    }
}