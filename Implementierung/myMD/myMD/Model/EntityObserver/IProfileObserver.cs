using myMD.Model.EntityObserver;
using myMD.Model.DataModel;

namespace myMD.Model.EntityObserver
{
	public interface IProfileObserver : IEntityObserver
	{
		void OnActivation(Profile profile);

	}

}

