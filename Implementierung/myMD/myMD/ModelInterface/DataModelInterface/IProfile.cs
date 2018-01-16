using System;

namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle f�r die Abstraktion eines Nutzerprofils, die die IEntity Schnittstelle erweitert.
    /// Ein Nutzerprofils enth�lt diverse Informationen �ber einen Nutzer.
    /// </summary>
	public interface IProfile : IEntity
	{
        /// <summary>
        /// Die Versicherungsnummer dieses Profils.
        /// </summary>
        string InsuranceNumber { get; set; }

        /// <summary>
        /// Der Nachname dieses Profils.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Die Blutgruppe dieses Profils.
        /// </summary>
        BloodType BloodType { get; set; }

        /// <summary>
        /// Das Geburtsdatum dieses Profils.
        /// </summary>
        DateTime BirthDate { get; set; }
	}

}

