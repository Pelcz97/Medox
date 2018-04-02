using myMD.ModelInterface.DataModelInterface;
using System.Collections.Generic;
using nexus.protocols.ble;
using System.Threading.Tasks;
using System;
using myMD.Model.MedicationInformation;

namespace myMD.ModelInterface.ModelFacadeInterface
{
    /// <summary>
    /// Dies ist die Haupteinstiegsstelle für andere Subsysteme in das Model.
    /// Als Ein- und Ausgabeparameter werden die restlichen Schnittstellen aus ModelInterface verwendet.
    /// Alle komplexeren Operationen, die nicht schon von diesen Schnittstellen übernommen werden, sind hier enthalten.
    /// </summary>
	public interface IModelFacade
    {
        /// <summary>
        /// Aktiviert das gegebene Profil.
        /// </summary>
        /// <param name="entity">Das zu aktivierende Profil</param>
        void Activate(IProfile profile);

        /// <summary>
        /// Legt eine neue Arztbriefgruppe an, die noch keine Arztbriefe enthält und gibt diese zurück.
        /// </summary>
        /// <returns>Die neu erstellte, leere Arztbriefgruppe</returns>
		IDoctorsLetterGroup CreateEmptyGroup();

        /// <summary>
        /// Legt eine neue Medikation an, die noch keine Informationen enthält und gibt diese zurück.
        /// </summary>
        /// <returns>Die neu erstellte, leere Medikation</returns>
		IMedication CreateEmptyMedication();

        /// <summary>
        /// Legt ein neues Profil an, das noch keine Informationen und Daten enthält und gibt dieses zurück.
        /// Wechselt außerdem das aktive Profil zu diesem Profil.
        /// </summary>
        /// <returns>Das neu erstellte Profil</returns>
		IProfile CreateEmptyProfile();

        /// <summary>
        /// Löscht die gegebene Entität.
        /// </summary>
        /// <param name="entity">Die zu löschende Entität</param>
        void Delete(IEntity entity);

        /// <summary>
        /// Gibt das aktive Profile zurück.
        /// </summary>
        /// <returns>Das zurzeit aktive Profil</returns>
        IProfile GetActiveProfile();

        /// <summary>
        /// Fordert alle Arztbriefe des aktiven Profils an und gibt diese zurück.
        /// </summary>
        /// <returns>Liste aller Arztbriefe des aktiven Profils</returns>
		IList<IDoctorsLetter> GetAllDoctorsLetters();

        /// <summary>
        /// Fordert alle Arztbriefgruppen des aktiven Profils an und gibt diese zurück.
        /// </summary>
        /// <returns>Liste aller Arztbriefgruppen des aktiven Profils</returns>
		IList<IDoctorsLetterGroup> GetAllGroups();

        /// <summary>
        /// Fordert alle Medikationen des aktiven Profils an und gibt diese zurück.
        /// </summary>
        /// <returns>Liste aller Medikationen des aktiven Profils</returns>
		IList<IMedication> GetAllMedications();

        /// <summary>
        /// Bereitet den gewünschten Arztbrief für die Datenübertragung vor und veranlasst diese anschließend.
        /// </summary>
        /// <param name="letter">Der zu sendende Arztbrief</param>
		//void SendLetter(IDoctorsLetter letter);

        /// <summary>
        /// Aktualisiert die gegebene Entität.
        /// </summary>
        /// <param name="entity">Die zu aktualisierende Entität</param>
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