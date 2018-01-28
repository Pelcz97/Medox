using myMD.Model.DatabaseModel;
using myMD.ModelInterface.DataModelInterface;
using System;
using System.IO;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Implementierung der IParserFacade Schnittstelle.
    /// Wählt beim Methodenaufruf einen passenden Parser aus diesem Paket auf und delegiert an diesen.
    /// </summary>
    /// <see>myMD.Model.ParserModel.IParserFacade</see>
	public class ParserFacade : IParserFacade
    {
        /// <summary>
        /// Wählt einen FileToDatabaseParser basierend auf dem Dateiformat von filename und delegiert an diesen.
        /// </summary>
        /// <see>myMD.Model.ParserModel.IParserFacade#ParseFileToDatabase(string, Model.DatabaseModel.IEntityDatabase)</see>
        public void ParseFileToDatabase(string filename, IEntityDatabase db)
        {
            string extension = Path.GetExtension(filename).TrimStart('.');
            if (Enum.TryParse(extension, out FileFormat format))
            {
                format.GetFileToDatabaseParser().ParseFile(filename, db);
            }
            else
            {
                throw new NotSupportedException($"Parsing '.{extension}' files is not supported");
            }
        }

        /// <summary>
        /// Delegiert an LetterToOriginalFileParser.
        /// </summary>
        /// <see>myMD.Model.ParserModel.IParserFacade#ParseLetterToOriginalFile(Model.DataModelInterface.IDoctorsLetter)</see>
        public string ParseLetterToOriginalFile(IDoctorsLetter letter)
        {
            return new LetterToOriginalFileParser().ParseLetter(letter);
        }
    }
}