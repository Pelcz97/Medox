using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using myMD.ViewModel.OverallViewModel;
using Plugin.BluetoothLE;

namespace myMDesktop.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Diese Klasse übernimmt die Auswahl des Gerätes an das die Arztbriefe gesendet werden sollen. Dazu wird die Umgebung nach Geräten gescannt, die dann in  <see cref="T:myMD.ViewModel.SendDataTabViewModel.ScanResultLetterViewModel"/> modeliert werden.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SelectDeviceViewModel : OverallViewModel
    {
        //public IAdapter BleAdapter { get; set; }

        /// <summary>
        /// Liste an Geräten die in der Umgebung gefunden wurden
        /// </summary>
        public ObservableCollection<ScanResultViewModel> DeviceList { get; }

        /// <summary>
        /// Konstruktor für ein SelectDeviceViewModel
        /// </summary>
        public SelectDeviceViewModel()
        {

            this.DeviceList = new ObservableCollection<ScanResultViewModel>();
            StartScan();
        }


        public async void StartScan()
        {
            BleAdapter.ScanWhenAdapterReady().Subscribe(scanResult =>
            {
                ScanResultViewModel test = new ScanResultViewModel();
                test.ScanResult = scanResult;

                if (scanResult != null && DeviceList.All(x => x.ScanResult.Device != test.ScanResult.Device))
                {
                    DeviceList.Add(test);
                }
            });
        }
    }
}
