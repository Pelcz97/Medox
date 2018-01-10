using Model.EntityObserver;
using Model.DataModel;

namespace Model.EntityObserver
{
	public interface IProfileObserver : IEntityObserver
	{
		void OnActivation(Profile profile);

	}

}

