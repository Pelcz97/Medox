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
        /// Methode um nach Geräten in der Nähe zu suchen
        /// </summary>
        /// <returns>Liste an Geräten</returns>
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
        /// Methode um sich mit einem andern Gerät zu koppeln
        /// </summary>
        /// <param name="device">Das andere Gerät</param>
        /// <param name="pin">Eine Art Passwort, um die Geräte zu verifizieren</param>
        /// <returns></returns>
		bool pair(IDevice device, string pin);

        /// <summary>
        /// Methode um sich mit einem anderen Gerät zu verbinden
        /// </summary>
        /// <param name="device">Das andere Gerät</param>
        /// <returns>true, falls die Geräte sich erfolgreich verbunden haben. Sonst false</returns>
		bool connect(IDevice device);

        /// <summary>
        /// Methode um daten zu Empfangen
        /// </summary>
        /// <returns></returns>
		string receive();

	}

}

