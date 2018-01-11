using Model.EntityObserver;
using Model.DataModel;
using System.Collections.Generic;
using ModelInterface.DataModelInterface;

namespace Model.DatabaseModel
{
	public interface IEntityDatabase : IEntityObserver
	{
		void Insert(Entity entity);

		IList<T> GetAllDataFromProfile<T>();

		IList<IProfile> GetAllProfiles();

		T GetEqual<T>(T entity);
	}

}

