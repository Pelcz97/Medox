using Model.DataModel;

namespace Model.EntityObserver
{
	public interface IEntityObserver
	{
		void OnUpdate(Entity entity);

		void OnDeletion(Entity entity);

	}

}

