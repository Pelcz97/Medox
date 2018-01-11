using System.Collections.Generic;
using System;

namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r eine Arztbriefgruppe, die die IData Schnittstelle erweitert.
    /// Eine Arztbriefgruppe ist eine Ansammlung von beliebig vielen Arztbriefen.
    /// </summary>
	public interface IDoctorsLetterGroup : IData
	{
        /// <summary>
        /// Gibt alle Arztbriefe in dieser Gruppe zur�ck.
        /// </summary>
        /// <returns>Liste aller Arztbriefe in dieser Gruppe</returns>
		IList<IDoctorsLetter> GetAll();

        /// <summary>
        /// F�gt einen Arztbrief zu dieser Gruppe hinzu. 
        /// Falls der Arztbrief bereits in der Gruppe enthalten ist, passiert nichts.
        /// </summary>
        /// <param name="letter">Der hinzuzuf�gende Arztbrief</param>
		void Add(IDoctorsLetter letter);

        /// <summary>
        /// Entfernt einen Arztbrief aus dieser Gruppe.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Werfe wenn der Arztbrief letter nicht in dieser Arztbriefgruppe enthalten ist</exception>
        /// <param name="letter">Der zu entfernende Arztbrief</param>
		void Remove(IDoctorsLetter letter);

        /// <summary>
        /// Gibt das aktuellste Datum aller Arztbriefe in dieser Gruppe zur�ck.
        /// </summary>
        /// <returns>Aktuellstes Datum aller Arztbriefe in dieser Gruppe</returns>
		DateTime GetLastDate();

	}

}

