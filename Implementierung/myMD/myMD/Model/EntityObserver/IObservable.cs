namespace myMD.Model.EntityObserver
{
	public interface IObservable<T>
	{
		void Subscribe(T observer);

        void Unsubscribe(T observer);
	}

}

