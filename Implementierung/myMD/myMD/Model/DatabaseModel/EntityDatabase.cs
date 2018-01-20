using myMD.Model.DataModel;
using myMD.Model.FileHelper;
using System.Collections.Generic;
using myMD.ModelInterface.DataModelInterface;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq;
using Xamarin.Forms;

namespace myMD.Model.DatabaseModel
{
    /// <summary>
    /// 
    /// </summary>
	public class EntityDatabase : IEntityDatabase
	{
        /// <summary>
        /// 
        /// </summary>
        public EntityDatabase()
        {
            this.fileHelper = DependencyService.Get<IFileHelper>();
            db = new SQLiteConnection(fileHelper.GetLocalFilePath(FILE));
            Create();
            profile = db.Find<Profile>(v => true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        public EntityDatabase(IFileHelper helper)
        {
            this.fileHelper = helper;
            db = new SQLiteConnection(fileHelper.GetLocalFilePath(FILE));
            Create();
            profile = db.Find<Profile>(v => true);
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly string FILE = "database.db3";

        /// <summary>
        /// 
        /// </summary>
        private SQLiteConnection db;

        /// <summary>
        /// 
        /// </summary>
        private Profile profile;

        /// <summary>
        /// 
        /// </summary>
		private IFileHelper fileHelper;


        /// <see>Model.EntityObserver.IEntityObserver#OnUpdate(Model.DataModel.Entity)</see>
        public void Update(IEntity entity) => db.UpdateWithChildren(entity.ToEntity());


        /// <see>Model.EntityObserver.IEntityObserver#OnDeletion(Model.DataModel.Entity)</see>
        public void Delete(IEntity entity)
        {
            Entity e = entity.ToEntity();
            e.Delete();
            db.Delete(e);
        }
 

        /// <see>Model.DatabaseModel.IEntityDatabase#Insert(Model.DataModel.Entity)</see>
        public void Insert(IEntity entity)
        {
            Entity e = entity.ToEntity();
            e.Profile = profile;
            e.ProfileID = profile.ID;
            db.Insert(e);
        }


        /// <see>Model.DatabaseModel.IEntityDatabase#GetAllDataFromProfile<T>()</see>
        public IList<E> GetAllDataFromProfile<E>() where E : Entity, new()
		{
            return db.GetAllWithChildren<E>(e => e.ProfileID == profile.ID, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<IDoctorsLetter> GetAllDoctorsLetters()
        {
            return GetAllDataFromProfile<DoctorsLetter>().Cast<IDoctorsLetter>().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<IDoctorsLetterGroup> GetAllDoctorsLetterGroups()
        {
            return GetAllDataFromProfile<DoctorsLetterGroup>().Cast<IDoctorsLetterGroup>().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        public void Destroy()
        {
            db.DropTable<DoctorsLetter>();
            db.DropTable<DoctorsLetterGroup>();
            db.DropTable<DoctorsLetterGroupDoctorsLetter>();
            db.DropTable<Medication>();
            db.DropTable<Profile>();
            db.DropTable<Doctor>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Create()
        {
            db.CreateTable<DoctorsLetter>();
            db.CreateTable<DoctorsLetterGroup>();
            db.CreateTable<DoctorsLetterGroupDoctorsLetter>();
            db.CreateTable<Medication>();
            db.CreateTable<Profile>();
            db.CreateTable<Doctor>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public IProfile GetProfile(IProfile profile) => db.Get<Profile>(v => v.InsuranceNumber.Equals(profile.InsuranceNumber));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDoctor GetDoctor(IDoctor doctor) => db.Get<Doctor>(v => v.Name.Equals(doctor.Name));
    }

}

