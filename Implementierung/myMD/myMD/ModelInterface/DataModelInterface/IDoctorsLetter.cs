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
        /// Fügt diesen Arztbrief zu einer Arztbriefgruppe hinzu.
        /// </summary>
        /// <param name="group">Die Gruppe zu der dieser Arztbrief hinzugefügt werden soll</param>
        void AddToGroup(IDoctorsLetterGroup group);

        /// <summary>
        /// Entfernt diesen Arztbrief aus einer Arztbriefgruppe.
        /// </summary>
        /// <param name="group">Die Gruppe aus der dieser Arztbrief entfernt werden soll</param>
        void RemoveFromGroup(IDoctorsLetterGroup group);

    }

}

