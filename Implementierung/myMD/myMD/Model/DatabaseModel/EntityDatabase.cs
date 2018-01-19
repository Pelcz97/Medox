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
        public EntityDatabase(IFileHelper fileHelper)
        {
            this.fileHelper = fileHelper;
            db = new SQLiteConnection(fileHelper.GetLocalFilePath(FILE));
            Create();
            profile = db.Find<Profile>(v => true);
        }

        private static readonly string FILE = "database.db3";

        private SQLiteConnection db;

        private Profile profile;

		private IFileHelper fileHelper;


        /// <see>Model.EntityObserver.IEntityObserver#OnUpdate(Model.DataModel.Entity)</see>
        public void Update(IEntity entity) => db.UpdateWithChildren(entity.ToEntity());


        /// <see>Model.EntityObserver.IEntityObserver#OnDeletion(Model.DataModel.Entity)</see>
        public void Delete(IEntity entity) => db.Delete(entity.ToEntity());


        /// <see>Model.DatabaseModel.IEntityDatabase#Insert(Model.DataModel.Entity)</see>
        public void Insert(IEntity entity) => db.Insert(entity.ToEntity());


        /// <see>Model.DatabaseModel.IEntityDatabase#GetAllDataFromProfile<T>()</see>
        public IList<E> GetAllDataFromProfile<E>() where E : Entity, new()
		{
            return db.GetAllWithChildren<E>(e => e.ProfileID == profile.ID, true);
        }

        public IList<IDoctorsLetter> GetAllDoctorsLetters()
        {
            return GetAllDataFromProfile<DoctorsLetter>().Cast<IDoctorsLetter>().ToList();
        }

        public IList<IDoctorsLetterGroup> GetAllDoctorsLetterGroups()
        {
            return GetAllDataFromProfile<DoctorsLetterGroup>().Cast<IDoctorsLetterGroup>().ToList();
        }

        public IList<IMedication> GetAllMedications()
        {
            return GetAllDataFromProfile<Medication>().Cast<IMedication>().ToList();
        }

        /// <see>Model.DatabaseModel.IEntityDatabase#GetAllProfiles()</see>
        public IList<IProfile> GetAllProfiles()
		{
            return db.GetAllWithChildren<Profile>().Cast<IProfile>().ToList();
        }

        /// <see>Model.EntityObserver.IProfileObserver#OnActivation(Model.DataModel.Profile)</see>
        public void Activate(IProfile profile) => this.profile = profile.ToProfile();

        public void Destroy()
        {
            db.DropTable<DoctorsLetter>();
            db.DropTable<DoctorsLetterGroup>();
            db.DropTable<DoctorsLetterGroupDoctorsLetter>();
            db.DropTable<Medication>();
            db.DropTable<Profile>();
            db.DropTable<Doctor>();
        }

        public void Create()
        {
            db.CreateTable<DoctorsLetter>();
            db.CreateTable<DoctorsLetterGroup>();
            db.CreateTable<DoctorsLetterGroupDoctorsLetter>();
            db.CreateTable<Medication>();
            db.CreateTable<Profile>();
            db.CreateTable<Doctor>();
        }

        public IProfile GetProfileFromInsuranceNumber(string number) => db.Get<Profile>(v => v.InsuranceNumber.Equals(number));

        public IDoctor GetDoctorFromName(string name) => db.Get<Doctor>(v => v.Name.Equals(name));
    }

}

