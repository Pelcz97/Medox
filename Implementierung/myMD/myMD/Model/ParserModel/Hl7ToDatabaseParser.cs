using myMD.Model.FileHelper;
using myMD.Model.DataModel;
using System.Collections.Generic;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// 
    /// </summary>
	public class Hl7ToDatabaseParser : FileToDatabaseParser
	{
        /// <summary>
        /// 
        /// </summary>
		private IFileHelper iFileHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DoctorsLetter ParseLetter(string filename)
		{
			return null;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IList<Medication> ParseMedications(string filename)
		{
			return null;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Doctor ParseDoctor(string filename)
		{
			return null;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Profile ParseProfile(string filename)
		{
			return null;
		}

	}

}

