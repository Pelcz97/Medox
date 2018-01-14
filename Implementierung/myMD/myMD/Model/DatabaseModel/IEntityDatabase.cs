using myMD.Model.EntityObserver;
using myMD.Model.DataModel;
using System.Collections.Generic;
using ModelInterface.DataModelInterface;

namespace myMD.Model.DatabaseModel
{
	public interface IEntityDatabase : IEntityObserver
	{
		void Insert(Entity entity);

        IList<E> GetAllDataFromProfile<E>() where E : Entity, new();

        IList<IProfile> GetAllProfiles();

		E GetEqual<E>(E entity) where E : Entity, new();
	}

}

