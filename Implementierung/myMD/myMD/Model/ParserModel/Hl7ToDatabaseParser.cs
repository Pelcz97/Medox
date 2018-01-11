using Model.FileHelper;
using Model.DataModel;
using System.Collections.Generic;

namespace Model.ParserModel
{
	public class Hl7ToDatabaseParser : FileToDatabaseParser
	{
		private IFileHelper iFileHelper;

        override
        protected DoctorsLetter ParseLetter()
		{
			return null;
		}

        override
		protected IList<Medication> ParseMedications()
		{
			return null;
		}

        override
        protected Doctor ParseDoctor()
		{
			return null;
		}

        override
        protected Profile ParseProfile()
		{
			return null;
		}

	}

}

