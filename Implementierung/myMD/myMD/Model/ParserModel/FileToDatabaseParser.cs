using Model.DatabaseModel;
using Model.DataModel;

namespace Model.ParserModel
{
	public abstract class FileToDatabaseParser
	{
		public void ParseFile(string filename, IEntityDatabase db)
		{

		}

		protected abstract DoctorsLetter ParseLetter();

		protected abstract Medication ParseMedications();

		protected abstract Doctor ParseDoctor();

		protected abstract Profile ParseProfile();

	}

}

