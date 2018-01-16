using ModelInterface.DataModelInterface;

namespace myMD.Model.EntityFactory
{
	public interface IEntityFactory
	{
		IMedication CreateEmptyMedication();

		IDoctorsLetterGroup CreateEmptyGroup();

		IProfile CreateEmptyProfile();

		IDoctorsLetter CreateEmptyDoctorsLetter();

	}

}

