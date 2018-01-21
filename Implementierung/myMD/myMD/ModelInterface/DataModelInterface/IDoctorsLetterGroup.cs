using myMD.Model.DataModel;
using System;
using System.Collections.Generic;

namespace myMD.ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für eine Arztbriefgruppe, die die IData Schnittstelle erweitert.
    /// Eine Arztbriefgruppe ist eine Ansammlung von beliebig vielen Arztbriefen.
    /// </summary>
	public interface IDoctorsLetterGroup : IData
    {
        /// <summary>
        /// Aktuellstes Datum aller Arztbriefe in dieser Gruppe.
        /// </summary>
        DateTime LastDate { get; }

        /// <summary>
        /// Liste aller Arztbriefe in dieser Gruppe.
        /// </summary>
		IList<IDoctorsLetter> DoctorsLetters { get; }

        /// <summary>
        /// Fügt einen Arztbrief zu dieser Gruppe hinzu.
        /// Falls der Arztbrief bereits in der Gruppe enthalten ist, passiert nichts.
        /// </summary>
        /// <param name="letter">Der hinzuzufügende Arztbrief</param>
        void Add(IDoctorsLetter letter);

        /// <summary>
        /// Entfernt einen Arztbrief aus dieser Gruppe.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Werfe wenn der Arztbrief letter nicht in dieser Arztbriefgruppe enthalten ist</exception>
        /// <param name="letter">Der zu entfernende Arztbrief</param>
		void Remove(IDoctorsLetter letter);

        /// <summary>
        /// Konvertiert die Schnittstelle zu ihrer Implementierung
        /// </summary>
        /// <returns>Konkrete Arztbriefgruppe</returns>
        DoctorsLetterGroup ToDoctorsLetterGroup();
    }
}