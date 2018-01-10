using Model.DataModelInterface;

namespace Model.EntityFactory
{
	public class IEntityFactory
	{
		public abstract IMedication CreateEmptyMedication();

		public abstract IDoctorsLetterGroup CreateEmptyGroup();

		public abstract IProfile CreateEmptyProfile();

		public abstract IDoctorsLetter CreateEmptyDoctorsLetter();

	}

}

