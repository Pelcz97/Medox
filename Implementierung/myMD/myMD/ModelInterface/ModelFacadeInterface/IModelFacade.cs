using myMD.ModelInterface.DataModelInterface;
using System.Collections.Generic;
using nexus.protocols.ble;
using System.Threading.Tasks;
using System;
using myMD.Model.MedicationInformation;

namespace myMD.ModelInterface.ModelFacadeInterface
{
    /// <summary>
    /// Dies ist die Haupteinstiegsstelle f�r andere Subsysteme in das Model.
    /// Als Ein- und Ausgabeparameter werden die restlichen Schnittstellen aus ModelInterface verwendet.
    /// Alle komplexeren Operationen, die nicht schon von diesen Schnittstellen �bernommen werden, sind hier enthalten.
    /// </summary>
	public interface IModelFacade
    {
        /// <summary>
        /// Aktiviert das gegebene Profil.
        /// </summary>
        /// <param name="entity">Das zu aktivierende Profil</param>
        void Activate(IProfile profile);

        /// <summary>
        /// Legt eine neue Arztbriefgruppe an, die noch keine Arztbriefe enth�lt und gibt diese zur�ck.
        /// </summary>
        /// <returns>Die neu erstellte, leere Arztbriefgruppe</returns>
		IDoctorsLetterGroup CreateEmptyGroup();

        /// <summary>
        /// Legt eine neue Medikation an, die noch keine Informationen enth�lt und gibt diese zur�ck.
        /// </summary>
        /// <returns>Die neu erstellte, leere Medikation</returns>
		IMedication CreateEmptyMedication();

        /// <summary>
        /// Legt ein neues Profil an, das noch keine Informationen und Daten enth�lt und gibt dieses zur�ck.
        /// Wechselt au�erdem das aktive Profil zu diesem Profil.
        /// </summary>
        /// <returns>Das neu erstellte Profil</returns>
		IProfile CreateEmptyProfile();

        /// <summary>
        /// L�scht die gegebene Entit�t.
        /// </summary>
        /// <param name="entity">Die zu l�schende Entit�t</param>
        void Delete(IEntity entity);

        /// <summary>
        /// Gibt das aktive Profile zur�ck.
        /// </summary>
        /// <returns>Das zurzeit aktive Profil</returns>
        IProfile GetActiveProfile();

        /// <summary>
        /// Fordert alle Arztbriefe des aktiven Profils an und gibt diese zur�ck.
        /// </summary>
        /// <returns>Liste aller Arztbriefe des aktiven Profils</returns>
		IList<IDoctorsLetter> GetAllDoctorsLetters();

        /// <summary>
        /// Fordert alle Arztbriefgruppen des aktiven Profils an und gibt diese zur�ck.
        /// </summary>
        /// <returns>Liste aller Arztbriefgruppen des aktiven Profils</returns>
		IList<IDoctorsLetterGroup> GetAllGroups();

        /// <summary>
        /// Fordert alle Medikationen des aktiven Profils an und gibt diese zur�ck.
        /// </summary>
        /// <returns>Liste aller Medikationen des aktiven Profils</returns>
		IList<IMedication> GetAllMedications();

        /// <summary>
        /// Bereitet den gew�nschten Arztbrief f�r die Daten�bertragung vor und veranlasst diese anschlie�end.
        /// </summary>
        /// <param name="letter">Der zu sendende Arztbrief</param>
		//void SendLetter(IDoctorsLetter letter);

        /// <summary>
        /// Aktualisiert die gegebene Entit�t.
        /// </summary>
        /// <param name="entity">Die zu aktualisierende Entit�t</param>
        void Update(IEntity entity);

        /// <summary>
        /// Setzt den Verbundenen GATT-Server.
        /// </summary>
        /// <param name="server">Server.</param>
        void SetConnectedServer(IBleGattServerConnection server);

        /// <summary>
        /// ?bergibt den aktuell verbundenen GATT-Server.
        /// </summary>
        /// <returns>The connected server.</returns>
        IBleGattServerConnection GetConnectedServer();

        /// <summary>
        /// Auslesen aller vom Server angebotenen Dateien.
        /// </summary>
        /// <returns>The files from server.</returns>
        Task GetFilesFromServer();

        /// <summary>
        /// Anzahl der vom Server angebotenen Dateien.
        /// </summary>
        /// <returns>The of files on server.</returns>
        Task<int> NumberOfFilesOnServer();

        Task<string> GetTextFromImage(byte[] image);

        void GenerateDoctorsLetter(string DoctorsName, string DoctorsField, DateTime LetterDate, string Diagnosis);

        Task<IList<InteractionPair>> GetInteractions(IList<IMedication> medications);

        Task<IList<DictionaryEntry>> GetDefinition(string expression);
    }
}