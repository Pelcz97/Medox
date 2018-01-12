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

        /// <summary>
        /// Da in einer Anwendung mehrere Profile existieren k�nnen, jedoch nicht mehrere Nutzer die Anwendung gleichzeitig verwenden k�nnen, muss es eine M�glichkeit geben zwischen den Profilen zu wechseln.
        /// Beim Aufruf dieser Methode wird dieses Profil als das momentan aktive Profil ausgew�hlt.
        /// </summary>
        void SetActive();

	}

}

