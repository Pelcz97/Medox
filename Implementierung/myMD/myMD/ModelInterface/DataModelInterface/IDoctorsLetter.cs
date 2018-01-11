using System.Collections.Generic;

namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r die Abstraktion eines Arztbriefes, die die IData Schnittstelle erweitert.
    /// Ein Arztbrief ist ein Transferdokument f�r die Kommunikation zwischen �rzten. Er gibt einen zusammenfassenden �berblick �ber den Status des Patienten bei der Entlassung,
    /// einen R�ckblick �ber den Krankheitsverlauf, die veranlasste Therapie und eine Interpretation des Geschehens zum Krankheitsverlauf im speziellen Fall.
    /// </summary>
	public interface IDoctorsLetter : IData
	{
        /// <summary>
        /// Gibt die Diagnose in diesem Arztbrief zur�ck.
        /// </summary>
        /// <returns>Diagnose in diesem Arztbrief</returns>
		string GetDiagnosis();

        /// <summary>
        /// Gibt den Arzt zur�ck, von dem dieser Arztbrief stammt.
        /// </summary>
        /// <returns>Arzt, von dem dieser Arztbrief stammt</returns>
		IDoctor GetDoctor();

        /// <summary>
        /// Gibt alle Medikationen zur�ck, die in diesem Arztbrief verschrieben wurden.
        /// </summary>
        /// <returns>Liste aller in diesem Arztbrief verschriebenen Medikationen</returns>
		IList<IMedication> GetMedication();

        /// <summary>
        /// L�st die Verbindung zwischen einer Medikation und diesem Arztbrief auf.
        /// </summary>
        /// <param name="med">Zu entferndene Medikation</param>
		void RemoveMedication(IMedication med);

        /// <summary>
        /// Verbindet eine Medikation mit diesem Arztbrief.
        /// Ist die Medikation bereits mit dem Arztbrief verbunden, so passiert nichts.
        /// </summary>
        /// <param name="med">Die zu verbindendene Medikation</param>
		void AttachMedication(IMedication med);

        /// <summary>
        /// Gibt alle Arztbriefgruppen zur�ck, in denen dieser Arztbrief enthalten ist, zur�ck.
        /// </summary>
        /// <returns>Liste aller Arztbriefgruppen, in denen dieser Arztbrief enthalten ist</returns>
		IList<IDoctorsLetterGroup> GetGroups();

	}

}

