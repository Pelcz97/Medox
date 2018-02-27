using System.Collections.Generic;
using myMD.ModelInterface.TransmissionModelInterface;
using nexus.protocols.ble;

namespace myMD.Model.TransmissionModel
{
    /// <summary>
    /// Klasse um alle Bluetoothvorg�nge zu kapseln
    /// </summary>
    public class Bluetooth : IBluetooth
    {

        public IBleGattServerConnection ConnectedGattServer { get; set; }

        /// <summary>
        /// Methode um nach Ger�ten in der N�he zu suchen
        /// </summary>
        /// <returns>Liste an Ger�ten</returns>
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
        /// Methode um sich mit einem andern Ger�t zu koppeln
        /// </summary>
        /// <param name="device">Das andere Ger�t</param>
        /// <param name="pin">Eine Art Passwort, um die Ger�te zu verifizieren</param>
        /// <returns></returns>
		public bool pair(IDevice device, string pin)
        {
            throw new System.NotImplementedException();

        }

        /// <summary>
        /// Methode um sich mit einem anderen Ger�t zu verbinden
        /// </summary>
        /// <param name="device">Das andere Ger�t</param>
        /// <returns>true, falls die Ger�te sich erfolgreich verbunden haben. Sonst false</returns>
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

