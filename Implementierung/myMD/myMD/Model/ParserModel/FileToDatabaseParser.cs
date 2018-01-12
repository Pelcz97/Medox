using myMD.Model.DatabaseModel;
using myMD.Model.DataModel;
using System.Collections.Generic;

namespace myMD.Model.ParserModel
{
	public abstract class FileToDatabaseParser
	{
		public void ParseFile(string filename, IEntityDatabase db)
		{

		}

		protected abstract DoctorsLetter ParseLetter();

		protected abstract IList<Medication> ParseMedications();

		protected abstract Doctor ParseDoctor();

		protected abstract Profile ParseProfile();

	}

}

