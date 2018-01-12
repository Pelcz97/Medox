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
        /// Die Diagnose in diesem Arztbrief.
        /// </summary>
		string Diagnosis { get; }

        /// <summary>
        /// Der Arzt, von dem dieser Arztbrief stammt.
        /// </summary>
        IDoctor Doctor { get; }

        /// <summary>
        /// Liste aller Medikationen, die in diesem Arztbrief verschrieben wurden.
        /// </summary>
        IList<IMedication> Medication { get; }

        /// <summary>
        /// Liste aller Arztbriefgruppen, in denen dieser Arztbrief enthalten ist.
        /// </summary>
        IList<IDoctorsLetterGroup> Groups { get; }

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
        /// F�gt diesen Arztbrief zu einer Arztbriefgruppe hinzu.
        /// </summary>
        /// <param name="group">Die Gruppe zu der dieser Arztbrief hinzugef�gt werden soll</param>
        void AddToGroup(IDoctorsLetterGroup group);

        /// <summary>
        /// Entfernt diesen Arztbrief aus einer Arztbriefgruppe.
        /// </summary>
        /// <param name="group">Die Gruppe aus der dieser Arztbrief entfernt werden soll</param>
        void RemoveFromGroup(IDoctorsLetterGroup group);

    }

}

