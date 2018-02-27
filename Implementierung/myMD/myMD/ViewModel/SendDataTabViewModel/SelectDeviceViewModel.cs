using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using Plugin.BluetoothLE;
using System.Threading;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Diese Klasse übernimmt die Auswahl des Gerätes an das die Arztbriefe gesendet werden sollen. Dazu wird die Umgebung nach Geräten gescannt, die dann in  <see cref="T:myMD.ViewModel.SendDataTabViewModel.ScanResultLetterViewModel"/> modeliert werden.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SelectDeviceViewModel : OverallViewModel.OverallViewModel
    {
        
        /// <summary>
        /// Liste an Geräten die in der Umgebung gefunden wurden
        /// </summary>
        public ObservableCollection<ScanResultViewModel> DeviceList { get; }

        /// <summary>
        /// Boolean, ob das Gerät des Nutzers gerade die Umbegung nach Geräten durchsucht
        /// </summary>
        private bool isScanning { get => CrossBleAdapter.Current.IsScanning; }


        public IDevice ConnectedDevice { get => ModelFacade.GetConnected(); }


        /// <summary>
        /// Konstruktor für ein SelectDeviceViewModel
        /// </summary>
        public SelectDeviceViewModel()
        {
            this.DeviceList = new ObservableCollection<ScanResultViewModel>();

            StartScan();
        }

        /// <summary>
        /// Methode um einen Scan zu starten.
        /// Dazu wird erst überprüft, ob gerade gescannt wird. Ist dies der Fall wird der aktive Scan abgebrochen.
        /// Dann wird geprüft ob Bluetooth für das Gerät verfügbar ist.
        /// Danach wird der Scan gestartet und die gefundenen Geräte in die Liste <see cref="T:myMD.ViewModel.SendDataTabViewModel.SelectDeviceLetterViewModel.DeviceList"/> gespeichert.
        /// </summary>
        public void StartScan()
        {
            if (isScanning == true)
            {
                
                DeviceList.Clear();
            }

            CrossBleAdapter.Current.ScanWhenAdapterReady().Subscribe(scanResult =>
            {
                
                ScanResultViewModel test = new ScanResultViewModel(scanResult);

                if (DeviceList.All(x => x.Device != test.Device) && scanResult.AdvertisementData.LocalName != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DeviceList.Add(test);
                        DeviceList.FirstOrDefault();
                    });
                }
            });
        }
                             


        public bool Connected { get; set; }

        /// <summary>
        /// Methode um sich mit einem anderen Gerät zu verbinden
        /// </summary>
        /// <param name="item"></param>
        public async Task ConnectToDevice(object item){
            var ScanResultItem = (ScanResultViewModel)item;
            IDevice device = ScanResultItem.Device;

            if (ScanResultItem.ScanResult.AdvertisementData.IsConnectable){
                Debug.WriteLine("Connectable");
            }

            ScanResultItem.ScanResult.Device.WhenStatusChanged().Subscribe(connectionState => {
                Debug.WriteLine("Connection State: " + connectionState);
            });

            try
            {
               
                if (ScanResultItem.Device.Status == ConnectionStatus.Disconnected)
                {
                    using (var cancelSrc = new CancellationTokenSource())
                    {
                        await device.Connect().ToTask(cancelSrc.Token);

                        if(device.Status == ConnectionStatus.Connected){
                            ScanResultItem.Device.WhenServiceDiscovered().Subscribe(service =>
                            {
                                Debug.WriteLine("Tapped _ " + service);
                            });
                            ModelFacade.SetConnected(device);
                        }
                    }
                } else {
                    Debug.WriteLine(ScanResultItem.Device.Status);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
