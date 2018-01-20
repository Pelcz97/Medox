using System;

namespace myMD.ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r die Abstraktion von Daten, die die IEntity Schnittstelle erweitert.
    /// Daten sind hier eine Ansammlung von medizinischen Informationen.
    /// </summary>
	public interface IData : IEntity
	{
        /// <summary>
        /// Der Zeitpunkt, von dem die Daten stammen.
        /// </summary>
		DateTime Date { get; }

        /// <summary>
        /// Die Sensitivit�tsstufe der Daten.
        /// </summary>
		Sensitivity Sensitivity { get; set; }
    }

}

