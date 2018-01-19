using myMD.Model.EntityObserver;
using myMD.Model.DataModel;
using System.Collections.Generic;
using ModelInterface.DataModelInterface;

namespace myMD.Model.DatabaseModel
{
	public interface IEntityDatabase
	{
		void Insert(IEntity entity);

        void Update(IEntity entity);

        void Delete(IEntity entity);

        void Activate(IProfile profile);

        IList<IProfile> GetAllProfiles();

        IList<IDoctorsLetter> GetAllDoctorsLetters();

        IList<IDoctorsLetterGroup> GetAllDoctorsLetterGroups();

        IList<IMedication> GetAllMedications();

        IProfile GetProfileFromInsuranceNumber(string number);

        IDoctor GetDoctorFromName(string name);
    }

}

