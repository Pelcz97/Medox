using Model.DatabaseModel;
using ModelInterface.DataModelInterface;

namespace Model.ParserModel
{
	public interface IParserFacade
	{
		void ParseFileToDatabase(string filename, IEntityDatabase db);

		string ParseLetterToOriginalFile(IDoctorsLetter letter);

	}

}

