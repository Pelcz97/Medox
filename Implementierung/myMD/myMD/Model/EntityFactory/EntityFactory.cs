using myMD.Model.DatabaseModel;
using myMD.ModelInterface.DataModelInterface;
using myMD.Model.DataModel;
using System;

namespace myMD.Model.EntityFactory
{
    /// <summary>
    /// 
    /// </summary>
	public class EntityFactory : IEntityFactory
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        public EntityFactory(IEntityDatabase database)
        {
            db = database;
        }

        /// <summary>
        /// 
        /// </summary>
		private IEntityDatabase db;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDoctorsLetter CreateEmptyDoctorsLetter()
        {
            DoctorsLetter letter = new DoctorsLetter
            {
                Name = "",
                Sensitivity = Sensitivity.Normal,
            };
            db.Insert(letter);
            return letter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDoctorsLetterGroup CreateEmptyGroup()
        {
            DoctorsLetterGroup group = new DoctorsLetterGroup
            {
                Name = "",
                Sensitivity = Sensitivity.Normal
            };
            db.Insert(group);
            return group;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IMedication CreateEmptyMedication()
        {
            Medication med = new Medication
            {
                Name = "",
                Sensitivity = Sensitivity.Normal,
                Date = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                Frequency = 1,
                Interval = Interval.Day
            };
            db.Insert(med);
            return med;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IProfile CreateEmptyProfile()
        {
            Profile profile = new Profile
            {
                Name = "",
                LastName = "",
                BirthDate = DateTime.Today,
                InsuranceNumber = "",
                BloodType = BloodType.ABMinus
            };
            db.Insert(profile);
            return profile;
        }
    }

}

