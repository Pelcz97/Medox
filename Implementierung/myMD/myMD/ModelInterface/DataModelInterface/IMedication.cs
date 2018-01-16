using System;
using myMD.Model.DataModel;

namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r die Abstraktion einer Medikation, die die IData Schnittstelle erweitert.
    /// Eine Medikation ist ein Stoff oder eine Stoffzusammensetzungen, die zur Heilung oder zur Verh�tung menschlicher oder tierischer Krankheiten bestimmt ist 
    /// oder sich dazu eignet physiologische Funktionen zu beeinflussen oder eine medizinische Diagnose zu erm�glichen.
    /// Sie kann von einem Arzt in einem Arztbrief verschrieben werden, f�r manche Medikationen wird dies jedoch nicht ben�tigt.
    /// </summary>
	public interface IMedication : IData
	{
        /// <summary>
        /// Das Datum an dem diese Medikation angefangen wurde
        /// </summary>
		new DateTime Date { get; set; }

        /// <summary>
        /// H�ufigkeit in der diese Medikation in ihrem Intervall genommen werden soll.
        /// Das Intervall und die H�ufigkeit zusammen ergeben dann eine tats�chliche H�ufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        int Frequency { get; set; }

        /// <summary>
        /// Das Zeitintervall in der diese Medikation eingenommen werden soll.
        /// Das Intervall und die H�ufigkeit zusammen ergeben dann eine tats�chliche H�ufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        Interval Interval { get; set; }

        /// <summary>
        /// Das Datum, an dem diese Medikation abgesetzt werden soll.
        /// </summary>
        DateTime EndDate { get; set; }

        /// <summary>
        /// L�st die Verbindung dieser Medikation zu einem Arztbrief auf.
        /// Besteht keine Verbindung zu diesem Arztbrief, so passiert nichts.
        /// </summary>
        /// <param name="letter">Der zu entfernende Arztbrief</param>
        void DisattachFromLetter(IDoctorsLetter letter);

        /// <summary>
        /// Verbindet diese Medikation mit einem Arztbrief.
        /// Ist der Arztbrief bereits verbunden, so passiert nichts.
        /// Existiert bereits eine Verbindung zu einem anderen Arztbrief, so wird diese zuvor aufgel�st.
        /// </summary>
        /// <param name="letter">Der zu verbindende Arztbrief</param>
		void AttachToLetter(IDoctorsLetter letter);

        Medication ToMedication();
	}
}

