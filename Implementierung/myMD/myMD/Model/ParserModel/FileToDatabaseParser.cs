using myMD.Model.DatabaseModel;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using System;
using System.Collections.Generic;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// 
    /// </summary>
	public abstract class FileToDatabaseParser
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="db"></param>
		public void ParseFile(string filename, IEntityDatabase db)
		{
            Profile profile = db.GetProfile(ParseProfile(filename)).ToProfile() ?? throw new InvalidOperationException("No matching Profile found");
            Doctor eqDoc = ParseDoctor(filename);
            Doctor doc = db.GetDoctor(eqDoc).ToDoctor();
            if(doc == null)
            {
                doc = eqDoc;
                db.Insert(doc);
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		protected abstract DoctorsLetter ParseLetter(string filename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		protected abstract IList<Medication> ParseMedications(string filename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		protected abstract Doctor ParseDoctor(string filename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
		protected abstract Profile ParseProfile(string filename);

	}

}

