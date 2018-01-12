using myMD.Model.EntityObserver;

namespace myMD.Model.EntityObserver
{
	public interface IProfileObservable : IEntityObservable
	{
		void Subscribe(IProfileObserver observer);

		void Unsubscribe(IProfileObserver observer);

	}

}

