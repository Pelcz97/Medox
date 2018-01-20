using myMD.ModelInterface.DataModelInterface;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// 
    /// </summary>
	public class LetterToOriginalFileParser
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
		public string ParseLetter(IDoctorsLetter letter) => letter.ToDoctorsLetter().Filepath;
    }

}

