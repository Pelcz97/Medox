using myMD.ModelInterface.TransmissionModelInterface;
using System.Collections.Generic;

namespace myMD.Model.TransmissionModel
{
    /// <summary>
    ///  Schnittstelle zur Bluetoothuebertragung
    /// </summary>
	public interface IBluetooth
	{
        /// <summary>
        /// Methode um nach Ger�ten in der N�he zu suchen
        /// </summary>
        /// <returns>Liste an Ger�ten</returns>
		IList<IDevice> scanForDevices();

        /// <summary>
        /// Methode um eine Datei zu senden
        /// </summary>
        /// <param name="filePath">Dateipfad zur Datei, die gesendet werden soll</param>
		void send(string filePath);

        /// <summary>
        /// Methode um einen Sendevorgang abzubrechen
        /// </summary>
		void cancelSend();

        /// <summary>
        /// Methode um sich mit einem andern Ger�t zu koppeln
        /// </summary>
        /// <param name="device">Das andere Ger�t</param>
        /// <param name="pin">Eine Art Passwort, um die Ger�te zu verifizieren</param>
        /// <returns></returns>
		bool pair(IDevice device, string pin);

        /// <summary>
        /// Methode um sich mit einem anderen Ger�t zu verbinden
        /// </summary>
        /// <param name="device">Das andere Ger�t</param>
        /// <returns>true, falls die Ger�te sich erfolgreich verbunden haben. Sonst false</returns>
		bool connect(IDevice device);

        /// <summary>
        /// Methode um daten zu Empfangen
        /// </summary>
        /// <returns></returns>
		string receive();

	}

}

