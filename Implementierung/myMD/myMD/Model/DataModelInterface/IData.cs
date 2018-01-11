using System;

namespace Model.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für die Abstraktion von Daten, die die IEntity Schnittstelle erweitert.
    /// Daten sind hier eine Ansammlung von medizinischen Informationen.
    /// </summary>
	public interface IData : IEntity
	{
        /// <summary>
        /// Gibt den Zeitpunkt zurück, von dem die Daten stammen.
        /// </summary>
        /// <returns>Zeitpunkt von dem die Daten stammen</returns>
		DateTime GetDate();

        /// <summary>
        /// Gibt die Sensitivitätsstufe der Daten zurück.
        /// </summary>
        /// <returns>Sensitivitätsstufe der Daten</returns>
		Sensitivity GetSensitivity();

        /// <summary>
        /// Ändert die Sensitivitätsstufe der Daten.
        /// </summary>
        /// <param name="sensitivity">Neue Sensitivitätsstufe der Daten</param>
		void SetSensitivity(Sensitivity sensitivity);

	}

}

