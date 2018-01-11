using System.Collections.Generic;

namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für die Abstraktion eines Arztbriefes, die die IData Schnittstelle erweitert.
    /// Ein Arztbrief ist ein Transferdokument für die Kommunikation zwischen Ärzten. Er gibt einen zusammenfassenden Überblick über den Status des Patienten bei der Entlassung,
    /// einen Rückblick über den Krankheitsverlauf, die veranlasste Therapie und eine Interpretation des Geschehens zum Krankheitsverlauf im speziellen Fall.
    /// </summary>
	public interface IDoctorsLetter : IData
	{
        /// <summary>
        /// Gibt die Diagnose in diesem Arztbrief zurück.
        /// </summary>
        /// <returns>Diagnose in diesem Arztbrief</returns>
		string GetDiagnosis();

        /// <summary>
        /// Gibt den Arzt zurück, von dem dieser Arztbrief stammt.
        /// </summary>
        /// <returns>Arzt, von dem dieser Arztbrief stammt</returns>
		IDoctor GetDoctor();

        /// <summary>
        /// Gibt alle Medikationen zurück, die in diesem Arztbrief verschrieben wurden.
        /// </summary>
        /// <returns>Liste aller in diesem Arztbrief verschriebenen Medikationen</returns>
		IList<IMedication> GetMedication();

        /// <summary>
        /// Löst die Verbindung zwischen einer Medikation und diesem Arztbrief auf.
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
        /// Gibt alle Arztbriefgruppen zurück, in denen dieser Arztbrief enthalten ist, zurück.
        /// </summary>
        /// <returns>Liste aller Arztbriefgruppen, in denen dieser Arztbrief enthalten ist</returns>
		IList<IDoctorsLetterGroup> GetGroups();

	}

}

