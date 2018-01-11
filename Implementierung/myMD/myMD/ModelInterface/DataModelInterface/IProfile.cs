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
        /// Gibt die Versicherungsnummer dieses Profils zur�ck.
        /// </summary>
        /// <returns>Versicherungsnummer dieses Profils</returns>
		string GetInsuranceNumber();

        /// <summary>
        /// �ndert die Versicherungsnummer dieses Profils.
        /// </summary>
        /// <param name="number">Neue Versicherungsnummer dieses Profils</param>
		void SetInsuranceNumber(string number);

        /// <summary>
        /// Gibt den Nachnamen dieses Profils zur�ck.
        /// </summary>
        /// <returns>Nachname des Profils</returns>
		string GetLastName();

        /// <summary>
        /// �ndert den Nachnamen dieses Profils.
        /// </summary>
        /// <param name="name">Neuer Nachname dieses Profils</param>
		void SetLastName(string name);

        /// <summary>
        /// Gibt die Blutgruppe dieses Profils zur�ck.
        /// </summary>
        /// <returns>Blutgruppe dieses Profils</returns>
		BloodType GetBloodType();

        /// <summary>
        /// �ndert die Blutgruppe dieses Profils.
        /// </summary>
        /// <param name="bloodType">Neue Blutgruppe dieses Profils</param>
		void SetBloodType(BloodType bloodType);

        /// <summary>
        /// Gibt das Geburtsdatum dieses Profils zur�ck
        /// </summary>
        /// <returns>Geburtsdatum dieses Profils</returns>
		DateTime GetBirthDate();

        /// <summary>
        /// �ndert das Geburtsdatum dieses Profils.
        /// </summary>
        /// <param name="date">Neues Geburtsdatum dieses Profils</param>
		void SetBirthDate(DateTime date);

        /// <summary>
        /// Da in einer Anwendung mehrere Profile existieren k�nnen, jedoch nicht mehrere Nutzer die Anwendung gleichzeitig verwenden k�nnen, muss es eine M�glichkeit geben zwischen den Profilen zu wechseln.
        /// Beim Aufruf dieser Methode wird dieses Profil als das momentan aktive Profil ausgew�hlt.
        /// </summary>
		void SetActive();

	}

}

