using Model.ParserModel;
using Model.FileHelper;
using Model.DataModel;
using zUtilities;

namespace Model.ParserModel
{
	public class Hl7ToDatabaseParser : FileToDatabaseParser
	{
		private IFileHelper iFileHelper;

		protected DoctorsLetter ParseLetter()
		{
			return null;
		}

		protected IList<Medication> ParseMedications()
		{
			return null;
		}

		protected Doctor ParseDoctor()
		{
			return null;
		}

		protected Profile ParseProfile()
		{
			return null;
		}

	}

}

