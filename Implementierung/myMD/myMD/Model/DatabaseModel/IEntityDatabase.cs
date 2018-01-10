using Model.EntityObserver;
using Model.DataModel;
using zUtilities;

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

