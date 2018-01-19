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

		protected abstract DoctorsLetter ParseLetter(string filename);

		protected abstract IList<Medication> ParseMedications(string filename);

		protected abstract Doctor ParseDoctor(string filename);

		protected abstract Profile ParseProfile(string filename);

	}

}

