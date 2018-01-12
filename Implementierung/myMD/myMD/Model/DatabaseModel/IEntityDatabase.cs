using myMD.Model.EntityObserver;
using myMD.Model.DataModel;
using System.Collections.Generic;
using ModelInterface.DataModelInterface;

namespace myMD.Model.DatabaseModel
{
	public interface IEntityDatabase : IEntityObserver
	{
		void Insert(Entity entity);

		IList<T> GetAllDataFromProfile<T>();

		IList<IProfile> GetAllProfiles();

		T GetEqual<T>(T entity);
	}

}

