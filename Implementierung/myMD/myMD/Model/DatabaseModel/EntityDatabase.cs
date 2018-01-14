using myMD.Model.EntityObserver;
using myMD.Model.DataModel;
using myMD.Model.FileHelper;
using System.Collections.Generic;
using ModelInterface.DataModelInterface;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;
using System.Linq;

namespace myMD.Model.DatabaseModel
{
	public class EntityDatabase : IEntityObserver, IEntityDatabase, IProfileObserver
	{
        EntityDatabase(string path)
        {
            db = new SQLiteConnection(path);
            db.CreateTable<DoctorsLetter>();
            db.CreateTable<DoctorsLetterGroup>();
            db.CreateTable<Medication>();
            db.CreateTable<Profile>();
            db.CreateTable<Doctor>();
        }

        private SQLiteConnection db;

        private Profile profile;

		private IFileHelper iFileHelper;


		/// <see>Model.EntityObserver.IEntityObserver#OnUpdate(Model.DataModel.Entity)</see>
		public void OnUpdate(Entity entity)
		{
            db.UpdateWithChildren(entity);
		}


		/// <see>Model.EntityObserver.IEntityObserver#OnDeletion(Model.DataModel.Entity)</see>
		public void OnDeletion(Entity entity)
		{
            entity.Unsubscribe(this);
            db.Delete(entity);
		}


		/// <see>Model.DatabaseModel.IEntityDatabase#Insert(Model.DataModel.Entity)</see>
		public void Insert(Entity entity)
		{
            entity.Subscribe(this);
            db.Insert(entity);
		}


		/// <see>Model.DatabaseModel.IEntityDatabase#GetAllDataFromProfile<T>()</see>
		public IList<E> GetAllDataFromProfile<E>() where E : Entity, new()
		{
            return db.GetAllWithChildren<E>(e => e.Profile.Equals(profile));
        }

        public IList<IDoctorsLetter> GetAllDoctorsLetters()
        {
            return db.GetAllWithChildren<DoctorsLetter>(letter => letter.Profile.Equals(profile)).Cast<IDoctorsLetter>().ToList();
        }

        public IList<IDoctorsLetterGroup> GetAllDoctorsLetterGroups()
        {
            return db.GetAllWithChildren<DoctorsLetterGroup>(group => group.Profile.Equals(profile)).Cast<IDoctorsLetterGroup>().ToList();
        }

        public IList<IMedication> GetAllMedications()
        {
            return db.GetAllWithChildren<Medication>(med => med.Profile.Equals(profile)).Cast<IMedication>().ToList();
        }

        /// <see>Model.DatabaseModel.IEntityDatabase#GetAllProfiles()</see>
        public IList<IProfile> GetAllProfiles()
		{
            return db.GetAllWithChildren<Profile>().Cast<IProfile>().ToList();
		}


		/// <see>Model.DatabaseModel.IEntityDatabase#GetEqual<T>(zUtilities.T)</see>
		public E GetEqual<E>(E entity) where E : Entity, new()
		{
            return db.FindWithChildren<E>(entity);
		}


		/// <see>Model.EntityObserver.IProfileObserver#OnActivation(Model.DataModel.Profile)</see>
		public void OnActivation(Profile profile)
		{
            this.profile = profile;
		}

	}

}

