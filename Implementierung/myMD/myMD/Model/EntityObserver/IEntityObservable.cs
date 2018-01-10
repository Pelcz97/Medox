using Model.EntityObserver;

namespace Model.EntityObserver
{
	public interface IEntityObservable
	{
		void Subscribe(IEntityObserver observer);

		void Unsubscribe(IEntityObserver observer);

	}

}

