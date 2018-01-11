using ModelInterface.DataModelInterface;
using Model.EntityObserver;

namespace Model.DataModel
{
	public class Entity : IEntity, IEntityObservable
	{
		private string name;

		private int id;

		private IEntityObserver iEntityObserver;

		public int GetID()
		{
			return 0;
		}

		public void SetID(int id)
		{

		}


		/// <see>Model.DataModelInterface.IEntity#GetName()</see>
		public string GetName()
		{
			return null;
		}


		/// <see>Model.DataModelInterface.IEntity#SetName(string)</see>
		public void SetName(string name)
		{

		}


		/// <see>Model.DataModelInterface.IEntity#Delete()</see>
		public void Delete()
		{

		}


		/// <see>Model.EntityObserver.IEntityObservable#Subscribe(Model.EntityObserver.IEntityObserver)</see>
		public void Subscribe(IEntityObserver observer)
		{

		}


		/// <see>Model.EntityObserver.IEntityObservable#Unsubscribe(Model.EntityObserver.IEntityObserver)</see>
		public void Unsubscribe(IEntityObserver observer)
		{

		}

	}

}

