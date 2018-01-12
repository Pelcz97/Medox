using myMD.Model.EntityObserver;

namespace myMD.Model.EntityObserver
{
	public interface IEntityObservable
	{
		void Subscribe(IEntityObserver observer);

		void Unsubscribe(IEntityObserver observer);

	}

}

