using myMD.Model.DatabaseModel;
using myMD.ModelInterface.DataModelInterface;
using System.IO;
using System;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// 
    /// </summary>
	public class ParserFacade : IParserFacade
    {
        /// <see>Model.ParserModel.IParserFacade#ParseFileToDatabase(string, Model.DatabaseModel.IEntityDatabase)</see>
        public void ParseFileToDatabase(string filename, IEntityDatabase db)
		{
            string extension = Path.GetExtension(filename);
            if (Enum.TryParse<FileFormat>(extension, out FileFormat format))
            {
                format.GetFileToDatabaseParser().ParseFile(filename, db);
            } else
            {
                throw new FormatException("Parsing '." + extension +"' files is not supported");
            }
        }

        /// <see>Model.ParserModel.IParserFacade#ParseLetterToOriginalFile(Model.DataModelInterface.IDoctorsLetter)</see>
        public string ParseLetterToOriginalFile(IDoctorsLetter letter)
        {
            return new LetterToOriginalFileParser().ParseLetter(letter);
        }
    }

}

