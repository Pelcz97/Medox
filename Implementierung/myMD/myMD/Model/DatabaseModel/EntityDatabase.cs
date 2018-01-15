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
	public class EntityDatabase : IEntityDatabase
	{
        public EntityDatabase(string path)
        {
            db = new SQLiteConnection(path);
            db.DropTable<DoctorsLetter>();
            db.DropTable<DoctorsLetterGroup>();
            db.DropTable<Medication>();
            db.DropTable<Profile>();
            db.DropTable<Doctor>();
            
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
		public void Update(Entity entity)
		{
            db.UpdateWithChildren(entity);
		}


		/// <see>Model.EntityObserver.IEntityObserver#OnDeletion(Model.DataModel.Entity)</see>
		public void Delete(Entity entity)
		{
            db.Delete(entity);
		}


		/// <see>Model.DatabaseModel.IEntityDatabase#Insert(Model.DataModel.Entity)</see>
		public void Insert(Entity entity)
		{
            db.Insert(entity);
		}


		/// <see>Model.DatabaseModel.IEntityDatabase#GetAllDataFromProfile<T>()</see>
		public IList<E> GetAllDataFromProfile<E>() where E : Entity, new()
		{
            return db.GetAllWithChildren<E>(e => e.ProfileID == profile.ID);
        }

        public IList<DoctorsLetter> GetAllDoctorsLetters()
        {
            return db.GetAllWithChildren<DoctorsLetter>(letter => letter.Profile.Equals(profile));
        }

        public IList<DoctorsLetterGroup> GetAllDoctorsLetterGroups()
        {
            return db.GetAllWithChildren<DoctorsLetterGroup>(group => group.Profile.Equals(profile));
        }

        public IList<Medication> GetAllMedications()
        {
            return db.GetAllWithChildren<Medication>(med => med.Profile.ID == profile.ID);
        }

        /// <see>Model.DatabaseModel.IEntityDatabase#GetAllProfiles()</see>
        public IList<Profile> GetAllProfiles()
		{
            return db.GetAllWithChildren<Profile>();
		}


		/// <see>Model.DatabaseModel.IEntityDatabase#GetEqual<T>(zUtilities.T)</see>
		public E GetEqual<E>(E entity) where E : Entity, new()
		{
            return db.FindWithChildren<E>(entity);
		}


		/// <see>Model.EntityObserver.IProfileObserver#OnActivation(Model.DataModel.Profile)</see>
		public void Activate(Profile profile)
		{
            this.profile = profile;
		}

        public void Destroy()
        {
            db.Dispose();
        }

	}

}

