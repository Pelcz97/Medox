using System;

namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für die Abstraktion einer Medikation, die die IData Schnittstelle erweitert.
    /// Eine Medikation ist ein Stoff oder eine Stoffzusammensetzungen, die zur Heilung oder zur Verhütung menschlicher oder tierischer Krankheiten bestimmt ist 
    /// oder sich dazu eignet physiologische Funktionen zu beeinflussen oder eine medizinische Diagnose zu ermöglichen.
    /// Sie kann von einem Arzt in einem Arztbrief verschrieben werden, für manche Medikationen wird dies jedoch nicht benötigt.
    /// </summary>
	public interface IMedication : IData
	{
        /// <summary>
        /// Ändert das Datum an dem diese Medikation angefangen wurde
        /// </summary>
        /// <param name="date">Das neue Startdatum dieser Medikation</param>
		void SetDate(DateTime date);

        /// <summary>
        /// Gibt zurück wie oft diese Medikation in ihrem Intervall genommen werden soll.
        /// Das Intervall und die Häufigkeit zusammen ergeben dann eine tatsächliche Häufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        /// <returns>Häufigkeit in der diese Medikation in ihrem Intervall genommen werden soll</returns>
		int GetFrequency();

        /// <summary>
        /// Ändert die Häufigkeit in der diese Medikation in ihrem Intervall genommen werden soll.
        /// Das Intervall und die Häufigkeit zusammen ergeben dann eine tatsächliche Häufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        /// <param name="freq">Neue Häufigkeit in der diese Medikation in ihrem Intervall genommen werden soll</param>
		void SetFrequency(int freq);

        /// <summary>
        /// Gibt zurück in welchem Zeitintervall die Medikation eingenommen werden soll. 
        /// Das Intervall und die Häufigkeit zusammen ergeben dann eine tatsächliche Häufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        /// <returns>Das Zeitintervall in der diese Medikation eingenommen werden soll.</returns>
		Interval GetInterval();

        /// <summary>
        /// Ändert das Zeitintervall in der diese Medikation eingenommen werden soll.
        /// Das Intervall und die Häufigkeit zusammen ergeben dann eine tatsächliche Häufigkeit in der die Medikation eingenommen werden soll (z.B. 3 mal pro Tag).
        /// </summary>
        /// <param name="interval">Neues Zeitintervall in der diese Medikation eingenommen werden soll</param>
		void SetInterval(Interval interval);

        /// <summary>
        /// Gibt das Datum zurück, an dem diese Medikation abgesetzt werden soll.
        /// </summary>
        /// <returns>Enddatum dieser Medikation</returns>
		DateTime GetEndDate();

        /// <summary>
        /// Ändert das Datum, an dem diese Medikation abgesetzt werden soll.
        /// </summary>
        /// <param name="date">Neues Enddatum dieser Medikation</param>
		void SetEndDate(DateTime date);

        /// <summary>
        /// Löst die Verbindung dieser Medikation zu ihrem Arztbrief auf.
        /// Besteht keine Verbindung zu einem Arztbrief, so passiert nichts.
        /// </summary>
		void DisattachFromLetter();

        /// <summary>
        /// Verbindet diese Medikation mit einem Arztbrief.
        /// Ist der Arztbrief bereits verbunden, so passiert nichts.
        /// Existiert bereits eine Verbindung zu einem anderen Arztbrief, so wird diese zuvor aufgelöst.
        /// </summary>
        /// <param name="letter">Der zu verbindende Arztbrief</param>
		void AttachToLetter(IDoctorsLetter letter);
	}

}

