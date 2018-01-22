using myMD.Model.DatabaseModel;
using myMD.Model.DataModel;
using myMD.ModelInterface.DataModelInterface;
using System;

namespace myMD.Model.EntityFactory
{
    /// <summary>
    /// Implementierung der IEntityFactory für die Klassen aus dem DataModel Paket.
    /// </summary>
    /// <see>myMD.Model.EntityFactory.IEntityFactory</see>
    /// <seealso>myMD.Model.DataModel</seealso>
	public class EntityFactory : IEntityFactory
    {
        /// <see>myMD.Model.EntityFactory.IEntityFactory#CreateEmptyDoctorsLetter()</see>
        public IDoctorsLetter CreateEmptyDoctorsLetter()
        {
            return new DoctorsLetter
            {
                Name = "",
                Sensitivity = Sensitivity.Normal,
            };
        }

        /// <see>myMD.Model.EntityFactory.IEntityFactory#CreateEmptyGroup()</see>
        public IDoctorsLetterGroup CreateEmptyGroup()
        {
            return new DoctorsLetterGroup
            {
                Name = "",
                Sensitivity = Sensitivity.Normal
            };
        }

        /// <see>myMD.Model.EntityFactory.IEntityFactory#CreateEmptyMedication()</see>
        public IMedication CreateEmptyMedication()
        {
            return new Medication
            {
                Name = "",
                Dosis = "",
                Sensitivity = Sensitivity.Normal,
                Date = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                Frequency = 1,
                Interval = Interval.Day
            };
        }

        /// <see>myMD.Model.EntityFactory.IEntityFactory#CreateEmptyProfile()</see>
        public IProfile CreateEmptyProfile()
        {
            return new Profile
            {
                Name = "",
                LastName = "",
                BirthDate = DateTime.Today,
                InsuranceNumber = "",
                BloodType = BloodType.ABMinus
            };
        }
    }
}