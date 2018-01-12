using myMD.Model.DataModel;

namespace myMD.Model.EntityObserver
{
	public interface IEntityObserver
	{
		void OnUpdate(Entity entity);

		void OnDeletion(Entity entity);

	}

}

