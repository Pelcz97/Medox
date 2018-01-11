using System;

namespace Model.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r die Abstraktion von Daten, die die IEntity Schnittstelle erweitert.
    /// Daten sind hier eine Ansammlung von medizinischen Informationen.
    /// </summary>
	public interface IData : IEntity
	{
        /// <summary>
        /// Gibt den Zeitpunkt zur�ck, von dem die Daten stammen.
        /// </summary>
        /// <returns>Zeitpunkt von dem die Daten stammen</returns>
		DateTime GetDate();

        /// <summary>
        /// Gibt die Sensitivit�tsstufe der Daten zur�ck.
        /// </summary>
        /// <returns>Sensitivit�tsstufe der Daten</returns>
		Sensitivity GetSensitivity();

        /// <summary>
        /// �ndert die Sensitivit�tsstufe der Daten.
        /// </summary>
        /// <param name="sensitivity">Neue Sensitivit�tsstufe der Daten</param>
		void SetSensitivity(Sensitivity sensitivity);

	}

}

