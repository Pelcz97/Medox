using myMD.Model.DataModel;
using myMD.Model.FileHelper;
using myMD.ModelInterface.DataModelInterface;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace myMD.Model.DatabaseModel
{
    /// <summary>
    /// Implementierung der IEntityDatabase Schnittstelle als Adapter zu einer SQLite Datenbank.
    /// </summary>
    /// <see>myMD.Model.DatabaseModel.IEntityDatabase</see>
	public class EntityDatabase : IEntityDatabase
    {
        /// <summary>
        /// Name der Datenbank-Datei.
        /// </summary>
        /// <see>myMD.Model.DatabaseModel.IEntityDatabase</see>
        private static readonly string FILE = "database.db3";

        /// <summary>
        /// Verbindung zur SQLite-Datenbank.
        /// </summary>
        /// <see>myMD.Model.DatabaseModel.IEntityDatabase</see>
        private SQLiteConnection db;

        /// <summary>
        /// Das zur Zeit aktive Profil.
        /// </summary>
        private Profile profile;

        /// <summary>
        /// Konstruktor mit automatisch nach Plattform ausgewähltem Dateihelfer.
        /// </summary>
        /// <see>myMD.Model.DatabaseModel.EntityDatabase#EntityDatabase(myMD.Model.FileHelper.IFileHelper)</see>
        public EntityDatabase() : this(DependencyService.Get<IFileHelper>()) { }

        /// <summary>
        /// Erstellt die Datenbank, falls sie noch nicht existiert, verbinet sich mit ihr und setzt das aktive Profil auf das erste Profil in der Datenbank.
        /// </summary>
        /// <param name="helper">Der Dateihelfer der verwendet werden soll um auf die Datenbankdatei zuzugreifen</param>
        public EntityDatabase(IFileHelper helper)
        {
            db = new SQLiteConnection(helper.GetLocalFilePath(FILE));
            Create();
            profile = db.Find<Profile>(v => true);
        }

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#Activate(myMD.ModelInterface.DataModelInterface.IProfile)</see>
        public void Activate(IProfile profile) => this.profile = profile.ToProfile();

        /// <summary>
        /// Erstellt die nötigen Datenbanktabellen.
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

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#Delete(ModelInterface.DataModelInterface.IEntity)</see>
        public void Delete(IEntity entity)
        {
            Entity e = entity.ToEntity();
            e.Delete();
            db.Delete(e);
        }

        /// <summary>
        /// Zerstört alle Datenbanktabellen.
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

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#GetActiveProfile()</see>
        public IProfile GetActiveProfile() => profile;

        /// <summary>
        /// Holt alle E des zur Zeit aktiven Profils aus der Datenbank.
        /// </summary>
        /// <typeparam name="E">Der Typ Entität der aus der Datenbank geholt werden soll</typeparam>
        /// <returns>Liste aller E des aktiven Profils</returns>
        public IList<E> GetAllDataFromProfile<E>() where E : Entity, new()
        {
            return db.GetAllWithChildren<E>(e => e.ProfileID == profile.ID, true);
        }

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#GetAllDoctorsLetterGroups()</see>
        public IList<IDoctorsLetterGroup> GetAllDoctorsLetterGroups()
        {
            return GetAllDataFromProfile<DoctorsLetterGroup>().Cast<IDoctorsLetterGroup>().ToList();
        }

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#GetAllDoctorsLetters()</see>
        public IList<IDoctorsLetter> GetAllDoctorsLetters()
        {
            return GetAllDataFromProfile<DoctorsLetter>().Cast<IDoctorsLetter>().ToList();
        }

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#GetAllMedications()</see>
        public IList<IMedication> GetAllMedications()
        {
            return GetAllDataFromProfile<Medication>().Cast<IMedication>().ToList();
        }

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#GetAllProfiles()</see>
        public IList<IProfile> GetAllProfiles()
        {
            return db.GetAllWithChildren<Profile>().Cast<IProfile>().ToList();
        }

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#GetDoctor(myMD.ModelInterface.DataModelInterface.IDoctor)</see>
        public IDoctor GetDoctor(IDoctor doctor) => db.Get<Doctor>(v => v.Name.Equals(doctor.Name));

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#GetProfile(myMD.ModelInterface.DataModelInterface.IProfile)</see>
        public IProfile GetProfile(IProfile profile) => db.Get<Profile>(v => v.InsuranceNumber.Equals(profile.InsuranceNumber));

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#Insert(ModelInterface.DataModelInterface.IEntity)</see>
        public void Insert(IEntity entity)
        {
            Entity e = entity.ToEntity();
            e.Profile = profile;
            db.Insert(e);
        }

        /// <see>myMD.Model.DatabaseModel.IEntityDatabase#Update(ModelInterface.DataModelInterface.IEntity)</see>
        public void Update(IEntity entity) => db.UpdateWithChildren(entity.ToEntity());
    }
}