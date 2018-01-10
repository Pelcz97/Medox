using Model.DatabaseModel;
using Model.DataModelInterface;

namespace Model.ParserModel
{
	public interface IParserFacade
	{
		void ParseFileToDatabase(string filename, IEntityDatabase db);

		string ParseLetterToOriginalFile(IDoctorsLetter letter);

	}

}

