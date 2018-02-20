using System;
using Plugin.BluetoothLE;
using Xamarin.Forms.Internals;

namespace myMDesktop.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Klasse die ein gefundenes Gerät modeliert
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ScanResultViewModel
    {
        public IScanResult ScanResult { get; set; }

        /// <summary>
        /// Der Name des Gerätes
        /// </summary>
        public string DeviceName { get => ScanResult.Device.Name; }

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
