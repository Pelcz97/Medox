using Model.DatabaseModel;
using Model.DataModelInterface;

namespace Model.EntityFactory
{
	public class EntityFactory : IEntityFactory
	{
		private EntityDatabase entityDatabase;

        public IDoctorsLetter CreateEmptyDoctorsLetter()
        {
            throw new System.NotImplementedException();
        }

        public IDoctorsLetterGroup CreateEmptyGroup()
        {
            throw new System.NotImplementedException();
        }

        public IMedication CreateEmptyMedication()
        {
            throw new System.NotImplementedException();
        }

        public IProfile CreateEmptyProfile()
        {
            throw new System.NotImplementedException();
        }
    }

}

