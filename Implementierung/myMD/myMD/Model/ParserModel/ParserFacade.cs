using myMD.Model.ParserModel;
using myMD.Model.DatabaseModel;
using ModelInterface.DataModelInterface;

namespace myMD.Model.ParserModel
{
	public class ParserFacade : IParserFacade
	{

		/// <see>Model.ParserModel.IParserFacade#ParseFileToDatabase(string, Model.DatabaseModel.IEntityDatabase)</see>
		public void ParseFileToDatabase(string filename, IEntityDatabase db)
		{

		}


		/// <see>Model.ParserModel.IParserFacade#ParseLetterToOriginalFile(Model.DataModelInterface.IDoctorsLetter)</see>
		public string ParseLetterToOriginalFile(IDoctorsLetter letter)
		{
			return null;
		}

	}

}

