using myMD.Model.DatabaseModel;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// 
    /// </summary>
	public interface IParserFacade
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="db"></param>
		void ParseFileToDatabase(string filename, IEntityDatabase db);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
		string ParseLetterToOriginalFile(IDoctorsLetter letter);

	}

}

