using Model.EntityObserver;
using Model.DataModel;
using Model.FileHelper;
using System.Collections.Generic;
using ModelInterface.DataModelInterface;

namespace Model.DatabaseModel
{
	public class EntityDatabase : IEntityObserver, IEntityDatabase, IProfileObserver
	{
		private Profile profile;

		private IFileHelper iFileHelper;


		/// <see>Model.EntityObserver.IEntityObserver#OnUpdate(Model.DataModel.Entity)</see>
		public void OnUpdate(Entity entity)
		{

		}


		/// <see>Model.EntityObserver.IEntityObserver#OnDeletion(Model.DataModel.Entity)</see>
		public void OnDeletion(Entity entity)
		{

		}


		/// <see>Model.DatabaseModel.IEntityDatabase#Insert(Model.DataModel.Entity)</see>
		public void Insert(Entity entity)
		{

		}


		/// <see>Model.DatabaseModel.IEntityDatabase#GetAllDataFromProfile<T>()</see>
		public IList<T> GetAllDataFromProfile<T>()
		{
			return null;
		}


		/// <see>Model.DatabaseModel.IEntityDatabase#GetAllProfiles()</see>
		public IList<IProfile> GetAllProfiles()
		{
			return null;
		}


		/// <see>Model.DatabaseModel.IEntityDatabase#GetEqual<T>(zUtilities.T)</see>
		public T GetEqual<T>(T entity)
		{
			return default(T);
		}


		/// <see>Model.EntityObserver.IProfileObserver#OnActivation(Model.DataModel.Profile)</see>
		public void OnActivation(Profile profile)
		{

		}

	}

}

