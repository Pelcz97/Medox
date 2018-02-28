using System;
using System.Collections.Generic;
using System.Diagnostics;
using nexus.protocols.ble.scan;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Klasse die ein gefundenes Gerät modeliert
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ScanResultViewModel
    {
        /*public ScanResultViewModel(IScanResult scanResult){
            this.ScanResult = scanResult;
        }
        
        public IScanResult ScanResult { get; set; }
        /// <summary>
        /// Das gefundene Gerät
        /// </summary>
        public IDevice Device { get => this.ScanResult.Device; }

        /// <summary>
        /// Der Name des Gerätes
        /// </summary>
        public string DeviceName { get => this.ScanResult.AdvertisementData.LocalName; }*/

        public ScanResultViewModel(IBlePeripheral scanResult)
        {
            this.ScanResult = scanResult;
        }

        public IBlePeripheral ScanResult { get; set; }

        public IEnumerable<Guid> services { get => this.ScanResult.Advertisement.Services; }

        /// <summary>
        /// Der Name des Gerätes
        /// </summary>
        public string DeviceName { get => this.ScanResult.Advertisement.DeviceName; }

        /// <summary>
        /// Boolean, ob man schon mit dem Gerät verbunden ist
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Guid des Gerätes
        /// </summary>
        public Guid Uuid { get => this.ScanResult.DeviceId; }

       
    }
}
