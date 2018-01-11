using System;

namespace ModelInterface.DataModelInterface
{
    /// <summary>
    /// Schnittstelle für die Abstraktion eines Nutzerprofils, die die IEntity Schnittstelle erweitert.
    /// Ein Nutzerprofils enthält diverse Informationen über einen Nutzer.
    /// </summary>
	public interface IProfile : IEntity
	{
        /// <summary>
        /// Gibt die Versicherungsnummer dieses Profils zurück.
        /// </summary>
        /// <returns>Versicherungsnummer dieses Profils</returns>
		string GetInsuranceNumber();

        /// <summary>
        /// Ändert die Versicherungsnummer dieses Profils.
        /// </summary>
        /// <param name="number">Neue Versicherungsnummer dieses Profils</param>
		void SetInsuranceNumber(string number);

        /// <summary>
        /// Gibt den Nachnamen dieses Profils zurück.
        /// </summary>
        /// <returns>Nachname des Profils</returns>
		string GetLastName();

        /// <summary>
        /// Ändert den Nachnamen dieses Profils.
        /// </summary>
        /// <param name="name">Neuer Nachname dieses Profils</param>
		void SetLastName(string name);

        /// <summary>
        /// Gibt die Blutgruppe dieses Profils zurück.
        /// </summary>
        /// <returns>Blutgruppe dieses Profils</returns>
		BloodType GetBloodType();

        /// <summary>
        /// Ändert die Blutgruppe dieses Profils.
        /// </summary>
        /// <param name="bloodType">Neue Blutgruppe dieses Profils</param>
		void SetBloodType(BloodType bloodType);

        /// <summary>
        /// Gibt das Geburtsdatum dieses Profils zurück
        /// </summary>
        /// <returns>Geburtsdatum dieses Profils</returns>
		DateTime GetBirthDate();

        /// <summary>
        /// Ändert das Geburtsdatum dieses Profils.
        /// </summary>
        /// <param name="date">Neues Geburtsdatum dieses Profils</param>
		void SetBirthDate(DateTime date);

        /// <summary>
        /// Da in einer Anwendung mehrere Profile existieren können, jedoch nicht mehrere Nutzer die Anwendung gleichzeitig verwenden können, muss es eine Möglichkeit geben zwischen den Profilen zu wechseln.
        /// Beim Aufruf dieser Methode wird dieses Profil als das momentan aktive Profil ausgewählt.
        /// </summary>
		void SetActive();

	}

}

