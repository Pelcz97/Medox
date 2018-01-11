using Model.DataModelInterface;

namespace Model.EntityFactory
{
	public interface IEntityFactory
	{
		IMedication CreateEmptyMedication();

		IDoctorsLetterGroup CreateEmptyGroup();

		IProfile CreateEmptyProfile();

		IDoctorsLetter CreateEmptyDoctorsLetter();

	}

}

