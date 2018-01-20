using myMD.Model.DatabaseModel;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using System;
using System.Collections.Generic;

namespace myMD.Model.ParserModel
{
	public abstract class FileToDatabaseParser
	{
		public void ParseFile(string filename, IEntityDatabase db)
		{
            Profile eqProfile = ParseProfile(filename);
            Profile profile = db.GetProfile(eqProfile).ToProfile();
            Doctor eqDoc = ParseDoctor(filename);
            Doctor doc = db.GetDoctor(eqDoc).ToDoctor();
            IList<Medication> meds = ParseMedications(filename);
            DoctorsLetter letter = ParseLetter(filename);
            db.Insert(letter);
            letter.Profile = profile;
            letter.DatabaseDoctor = doc;
            foreach (Medication med in meds)
            {              
                db.Insert(med);
                letter.AttachMedication(med);
            }
            db.Update(letter);
        }

		protected abstract DoctorsLetter ParseLetter(string filename);

		protected abstract IList<Medication> ParseMedications(string filename);

		protected abstract Doctor ParseDoctor(string filename);

		protected abstract Profile ParseProfile(string filename);

	}

}

