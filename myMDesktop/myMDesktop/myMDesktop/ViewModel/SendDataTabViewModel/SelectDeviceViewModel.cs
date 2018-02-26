using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using myMD.ViewModel.OverallViewModel;
using Plugin.BluetoothLE;
using Xamarin.Forms.PlatformConfiguration;
using Windows.UI.Core;
using System.Threading;
using System.Reactive.Threading.Tasks;

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

            CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
            {
                ScanResultViewModel test = new ScanResultViewModel();
                test.ScanResult = scanResult;
                if (scanResult != null && DeviceList.All(x => x.ScanResult.Device != scanResult.Device) && scanResult.Device.Name != null)
                { 
                    
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        DeviceList.Add(test);
                        DeviceList.FirstOrDefault();
                    });
                }
            });
        }

        public async void ConnectToDevice(object item)
        {
            var ScanResultItem = (ScanResultViewModel)item;
            IDevice device = ScanResultItem.ScanResult.Device;

            if (ScanResultItem.ScanResult.AdvertisementData.IsConnectable)
            {
                Debug.WriteLine("Connectable");
            }

            ScanResultItem.ScanResult.Device.WhenAnyDescriptorDiscovered().Subscribe(service =>
            {
                Debug.WriteLine(service.Description);
            });

            ScanResultItem.ScanResult.Device.WhenAnyCharacteristicDiscovered().Subscribe(service =>
            {
                Debug.WriteLine(service.Uuid);
            });

            ScanResultItem.ScanResult.Device.WhenStatusChanged().Subscribe(connectionState => {
                Debug.WriteLine("Connection State: " + connectionState);
            });

            try
            {
                // don't cleanup connection - force user to d/c
                if (ScanResultItem.ScanResult.Device.Status == ConnectionStatus.Disconnected)
                {
                    using (var cancelSrc = new CancellationTokenSource())
                    {
                        await device.Connect().ToTask(cancelSrc.Token);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

    }
}
