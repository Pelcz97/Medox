using myMD.ModelInterface.DataModelInterface;
using System.Collections.Generic;

namespace myMD.Model.DatabaseModel
{
    /// <summary>
    /// Schnittstelle zur Interaktion mit einer Datenbank, in der alle Entitäten aus dem DataModelInterface Paket gespeichert werden können.
    /// </summary>
    /// <seealso>myMD.ModelInterface.DataModelInterface</seealso>
	public interface IEntityDatabase
    {
        /// <summary>
        /// Aktiviert ein Profil.
        /// </summary>
        /// <param name="profile">Das zu aktivierende Profil</param>
        void Activate(IProfile profile);

        /// <summary>
        /// Löscht eine Entität aus der Datenbank.
        /// </summary>
        /// <param name="entity">Die zu löschende Entität</param>
        void Delete(IEntity entity);

        /// <summary>
        /// Gibt das aktive Profile zurück.
        /// </summary>
        /// <returns>Das zurzeit aktive Profil</returns>
        IProfile GetActiveProfile();

        /// <summary>
        /// Holt alle Arztbriefgruppen des zur Zeit aktiven Profils aus der Datenbank.
        /// </summary>
        /// <returns>Liste aller Arztbriefgruppen des aktiven Profils</returns>
        IList<IDoctorsLetterGroup> GetAllDoctorsLetterGroups();

        /// <summary>
        /// Holt alle Arztbriefe des zur Zeit aktiven Profils aus der Datenbank.
        /// </summary>
        /// <returns>Liste aller Arztbriefe des aktiven Profils</returns>
        IList<IDoctorsLetter> GetAllDoctorsLetters();

        /// <summary>
        /// Holt alle Medikationen des zur Zeit aktiven Profils aus der Datenbank.
        /// </summary>
        /// <returns>Liste aller Medikationen des aktiven Profils</returns>
        IList<IMedication> GetAllMedications();

        /// <summary>
        /// Holt alle Profile aus der Datenbank.
        /// </summary>
        /// <returns>Liste aller in der Datenbank gespeicherten Profile</returns>
        IList<IProfile> GetAllProfiles();

        /// <summary>
        /// Suche nach ähnlichem Arzt in der Datenbank.
        /// </summary>
        /// <param name="doctor">Suche nach Arzt der ähnlich zu diesem Arzt ist</param>
        /// <returns>Ähnlicher Arzt aus der Datenbank oder null falls kein solcher Arzt existiert</returns>
        IDoctor GetDoctor(IDoctor doctor);

        /// <summary>
        /// Suche nach ähnlichem Profil in der Datenbank.
        /// </summary>
        /// <param name="profile">Suche nach Profil das ähnlich zu diesem Profil ist</param>
        /// <returns>Ähnliches Profil aus der Datenbank oder null falls kein solches Profil existiert</returns>
        IProfile GetProfile(IProfile profile);

        /// <summary>
        /// Fügt eine Entität in die Datenbank zu dem zur Zeit aktiven Profil ein.
        /// </summary>
        /// <param name="entity">Die einzufügende Entität</param>
		void Insert(IEntity entity);

        /// <summary>
        /// Aktualisiert eine Entität in der Datenbank.
        /// </summary>
        /// <param name="entity">Die zu aktualisierende Entität</param>
        void Update(IEntity entity);
    }
}