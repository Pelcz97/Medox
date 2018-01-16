using myMD.Model.EntityObserver;
using myMD.Model.DataModel;
using System.Collections.Generic;
using ModelInterface.DataModelInterface;

namespace myMD.Model.DatabaseModel
{
	public interface IEntityDatabase
	{
		void Insert(Entity entity);

        void Update(Entity entity);

        void Delete(Entity entity);

        void Activate(Profile profile);

        IList<E> GetAllDataFromProfile<E>() where E : Entity, new();

        IList<Profile> GetAllProfiles();

		E GetEqual<E>(E entity) where E : Entity, new();
	}

}

