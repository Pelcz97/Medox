using Model.EntityObserver;

namespace Model.EntityObserver
{
	public interface IProfileObservable : IEntityObservable
	{
		void Subscribe(IProfileObserver observer);

		void Unsubscribe(IProfileObserver observer);

	}

}

