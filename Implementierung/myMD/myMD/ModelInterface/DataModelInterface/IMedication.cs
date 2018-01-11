using System;

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
        /// �ndert das Datum an dem diese Medikation angefangen wurde
        /// </summary>
        /// <param name="date">Das neue Startdatum dieser Medikation</param>
		void SetDate(DateTime date);

        /// <summary>
        /// Gibt zur�ck wie oft diese Medikation in ihrem Intervall genommen werden soll.
        /// Das Intervall und die H�ufigkeit zusammen ergeben dann eine tats�chliche H�ufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        /// <returns>H�ufigkeit in der diese Medikation in ihrem Intervall genommen werden soll</returns>
		int GetFrequency();

        /// <summary>
        /// �ndert die H�ufigkeit in der diese Medikation in ihrem Intervall genommen werden soll.
        /// Das Intervall und die H�ufigkeit zusammen ergeben dann eine tats�chliche H�ufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        /// <param name="freq">Neue H�ufigkeit in der diese Medikation in ihrem Intervall genommen werden soll</param>
		void SetFrequency(int freq);

        /// <summary>
        /// Gibt zur�ck in welchem Zeitintervall die Medikation eingenommen werden soll. 
        /// Das Intervall und die H�ufigkeit zusammen ergeben dann eine tats�chliche H�ufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        /// <returns>Das Zeitintervall in der diese Medikation eingenommen werden soll.</returns>
		Interval GetInterval();

        /// <summary>
        /// �ndert das Zeitintervall in der diese Medikation eingenommen werden soll.
        /// Das Intervall und die H�ufigkeit zusammen ergeben dann eine tats�chliche H�ufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        /// <param name="interval">Neues Zeitintervall in der diese Medikation eingenommen werden soll</param>
		void SetInterval(Interval interval);

        /// <summary>
        /// Gibt das Datum zur�ck, an dem diese Medikation abgesetzt werden soll.
        /// </summary>
        /// <returns>Enddatum dieser Medikation</returns>
		DateTime GetEndDate();

        /// <summary>
        /// �ndert das Datum, an dem diese Medikation abgesetzt werden soll.
        /// </summary>
        /// <param name="date">Neues Enddatum dieser Medikation</param>
		void SetEndDate(DateTime date);

        /// <summary>
        /// L�st die Verbindung dieser Medikation zu ihrem Arztbrief auf.
        /// Besteht keine Verbindung zu einem Arztbrief, so passiert nichts.
        /// </summary>
		void DisattachFromLetter();

        /// <summary>
        /// Verbindet diese Medikation mit einem Arztbrief.
        /// Ist der Arztbrief bereits verbunden, so passiert nichts.
        /// Existiert bereits eine Verbindung zu einem anderen Arztbrief, so wird diese zuvor aufgel�st.
        /// </summary>
        /// <param name="letter">Der zu verbindende Arztbrief</param>
		void AttachToLetter(IDoctorsLetter letter);
	}

}

