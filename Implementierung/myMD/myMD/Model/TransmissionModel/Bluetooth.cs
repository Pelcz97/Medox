using System.Collections.Generic;
using myMD.ModelInterface.TransmissionModelInterface;
using nexus.protocols.ble;

namespace myMD.Model.TransmissionModel
{
    /// <summary>
    /// Klasse um alle Bluetoothvorgänge zu kapseln
    /// </summary>
    public class Bluetooth : IBluetooth
    {

        public IBleGattServerConnection ConnectedGattServer { get; set; }

        /// <summary>
        /// Methode um nach Geräten in der Nähe zu suchen
        /// </summary>
        /// <returns>Liste an Geräten</returns>
        public IList<IDevice> scanForDevices()
        {
            throw new System.NotImplementedException();
        }



        /// <summary>
        /// Methode um eine Datei zu senden
        /// </summary>
        /// <param name="filePath">Dateipfad zur Datei, die gesendet werden soll</param>
		public void send(string filePath)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Methode um einen Sendevorgang abzubrechen
        /// </summary>
		public void cancelSend()
        {
            throw new System.NotImplementedException();

        }

        /// <summary>
        /// Methode um sich mit einem andern Gerät zu koppeln
        /// </summary>
        /// <param name="device">Das andere Gerät</param>
        /// <param name="pin">Eine Art Passwort, um die Geräte zu verifizieren</param>
        /// <returns></returns>
		public bool pair(IDevice device, string pin)
        {
            throw new System.NotImplementedException();

        }

        /// <summary>
        /// Methode um sich mit einem anderen Gerät zu verbinden
        /// </summary>
        /// <param name="device">Das andere Gerät</param>
        /// <returns>true, falls die Geräte sich erfolgreich verbunden haben. Sonst false</returns>
		public bool connect(IDevice device)
        {
            throw new System.NotImplementedException();

        }

        /// <summary>
        /// Methode um daten zu Empfangen
        /// </summary>
        /// <returns></returns>
		public string receive()
        {
            throw new System.NotImplementedException();

        }
    }

}

