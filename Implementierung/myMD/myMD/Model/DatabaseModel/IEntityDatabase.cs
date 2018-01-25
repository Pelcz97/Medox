using myMD.ModelInterface.DataModelInterface;
using System.Collections.Generic;

namespace myMD.Model.DatabaseModel
{
    /// <summary>
    /// Schnittstelle zur Interaktion mit einer Datenbank, in der alle Entit�ten aus dem DataModelInterface Paket gespeichert werden k�nnen.
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
        /// L�scht eine Entit�t aus der Datenbank.
        /// </summary>
        /// <param name="entity">Die zu l�schende Entit�t</param>
        void Delete(IEntity entity);

        /// <summary>
        /// Gibt das aktive Profile zur�ck.
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
        /// Suche nach �hnlichem Arzt in der Datenbank.
        /// </summary>
        /// <param name="doctor">Suche nach Arzt der �hnlich zu diesem Arzt ist</param>
        /// <returns>�hnlicher Arzt aus der Datenbank oder null falls kein solcher Arzt existiert</returns>
        IDoctor GetDoctor(IDoctor doctor);

        /// <summary>
        /// Suche nach �hnlichem Profil in der Datenbank.
        /// </summary>
        /// <param name="profile">Suche nach Profil das �hnlich zu diesem Profil ist</param>
        /// <returns>�hnliches Profil aus der Datenbank oder null falls kein solches Profil existiert</returns>
        IProfile GetProfile(IProfile profile);

        /// <summary>
        /// F�gt eine Entit�t in die Datenbank zu dem zur Zeit aktiven Profil ein.
        /// </summary>
        /// <param name="entity">Die einzuf�gende Entit�t</param>
		void Insert(IEntity entity);

        /// <summary>
        /// Aktualisiert eine Entit�t in der Datenbank.
        /// </summary>
        /// <param name="entity">Die zu aktualisierende Entit�t</param>
        void Update(IEntity entity);
    }
}