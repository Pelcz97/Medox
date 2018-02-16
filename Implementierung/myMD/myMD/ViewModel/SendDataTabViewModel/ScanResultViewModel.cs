using System;
using System.Diagnostics;
using nexus.protocols.ble.scan;
using Plugin.BluetoothLE;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Klasse die ein gefundenes Gerät modeliert
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ScanResultViewModel
    {
        /// <summary>
        /// Das gefundene Gerät
        /// </summary>
        public IBlePeripheral Device { get; set; }
        public string test { get; set; }
        /// <summary>
        /// Der Name des Gerätes
        /// </summary>
        public string DeviceName { 
            get {
                Debug.WriteLine("#############################");
                Debug.WriteLine("DeviceName: " + this.Device.Advertisement.DeviceName);
                Debug.WriteLine("Flags: " + this.Device.Advertisement.Flags);
                Debug.WriteLine("HashCode: " + this.Device.Advertisement.GetHashCode());
                Debug.WriteLine("ManufactureData: " + this.Device.Advertisement.ManufacturerSpecificData.ToString());
                Debug.WriteLine("RawData: " + this.Device.Advertisement.RawData.ToString());

                this.Device.Advertisement.ServiceData.ForEach((obj) =>
                {
                    test = obj.Key.ToString();
                });

                Debug.WriteLine("ServiceData test: " + test);
                Debug.WriteLine("Services: " + this.Device.Advertisement.Services);
                Debug.WriteLine("Address: " + this.Device.Address.ToString());
                Debug.WriteLine("DeviceID: " + this.Device.DeviceId);
                Debug.WriteLine("#############################");

                return this.Device.Advertisement.DeviceName;

            }
        }

        /// <summary>
        /// Boolean, ob man schon mit dem Gerät verbunden ist
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Guid des Gerätes
        /// </summary>
        public Guid Uuid { get; private set; }

        /// <summary>
        /// Localer Name des Gerätes
        /// </summary>
        public string LocalName { get; private set; }
    }
}
