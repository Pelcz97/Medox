using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using Plugin.BluetoothLE;
using System.Threading;
using System.Reactive.Threading.Tasks;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Diese Klasse übernimmt die Auswahl des Gerätes an das die Arztbriefe gesendet werden sollen. Dazu wird die Umgebung nach Geräten gescannt, die dann in  <see cref="T:myMD.ViewModel.SendDataTabViewModel.ScanResultLetterViewModel"/> modeliert werden.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SelectDeviceViewModel : OverallViewModel.OverallViewModel
    {
        //public IAdapter BleAdapter { get; set; }
        //public static Guid myMDguid = new Guid("00000000-1000-1000-1000-00805F9B0000");
        public static Guid myMDserviceGuid1 = new Guid("10000000-1000-1000-1000-100000000000");
        //public static Guid myMDserviceGuid2 = new Guid("20000000-2000-2000-2000-200000000000");
        public static Guid myMDcharGuid1 = new Guid("30000000-3000-3000-3000-300000000000");
        //public static Guid myMDcharGuid2 = new Guid("40000000-4000-4000-4000-400000000000");

        /// <summary>
        /// Liste an Geräten die in der Umgebung gefunden wurden
        /// </summary>
        public ObservableCollection<ScanResultViewModel> DeviceList { get; }

        /// <summary>
        /// Boolean, ob das Gerät des Nutzers gerade die Umbegung nach Geräten durchsucht
        /// </summary>
        private bool isScanning { get; set; }

        /// <summary>
        /// Ergebniss des Scans
        /// </summary>
        public IDisposable scan { get; set; }

        IDisposable notifier;

        public IScanResult ConnectedDevice { get; set; }


        /// <summary>
        /// Konstruktor für ein SelectDeviceViewModel
        /// </summary>
        public SelectDeviceViewModel()
        {
            this.DeviceList = new ObservableCollection<ScanResultViewModel>();
            ConnectedDevice = null;


            BluetoothAdapter.WhenStatusChanged().Subscribe(state =>
            {
                Debug.WriteLine("New State: {0}", state);
                if (state == AdapterStatus.PoweredOn)
                {
                    StartScan();
                }
            });
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
                scan.Dispose();
                DeviceList.Clear();
            }

            CrossBleAdapter.Current.WhenStatusChanged().Subscribe(x => {
                Debug.WriteLine(x);
            });

            isScanning = true;


            this.scan = CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
            {
                ScanResultViewModel test = new ScanResultViewModel(scanResult);

                if (test != null && DeviceList.All(x => x.Device != test.Device) && scanResult.AdvertisementData.LocalName != null)
                {
                    DeviceList.Add(test);
                    DeviceList.FirstOrDefault();


                    Debug.WriteLine("Neu_LocalName " + scanResult.AdvertisementData.LocalName);
                    Debug.WriteLine("Neu_Connectable " + scanResult.AdvertisementData.IsConnectable);
                    Debug.WriteLine("Neu_ManuData " + scanResult.AdvertisementData.ManufacturerData);
                    Debug.WriteLine("Neu_ServiceData " + scanResult.AdvertisementData.ServiceData);
                    Debug.WriteLine("Neu_ServiceUUID " + scanResult.AdvertisementData.ServiceUuids);
                    Debug.WriteLine("Neu_ServiceUUID_Count " + scanResult.AdvertisementData.ServiceUuids.Count());

                    Debug.WriteLine("Device.Name: " + test.Device.Name);
                    Debug.WriteLine("Device.UUID: " + test.Device.Uuid);
                }
            });
        }
            
        /// <summary>
        /// Methode um einen Scan zu stoppen.
        /// </summary>
        public void StopScan()
        {
            if (isScanning)
            {
                this.scan.Dispose();
            }
            isScanning = false;
        }
                             



        /// <summary>
        /// Methode um sich mit einem anderen Gerät zu verbinden
        /// </summary>
        /// <param name="item"></param>
        public async void ConnectToDevice(object item){
            var ScanResultItem = (ScanResultViewModel)item;
            IDevice device = ScanResultItem.Device;

            if (ScanResultItem.ScanResult.AdvertisementData.IsConnectable){
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
                if (ScanResultItem.Device.Status == ConnectionStatus.Disconnected)
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

            /*try {

                ScanResultItem.ScanResult.Device.Connect();

                device.Connect(new GattConnectionConfig
                {
                    NotifyOnConnect = true
                });

                ScanResultItem.ScanResult.Device.WhenStatusChanged().Subscribe(connectionState => { 
                    Debug.WriteLine("Connection State: " + connectionState);
                });
            } 
            catch (Exception ex){
                Debug.WriteLine(ex);
            }*/
        }



        public void TargetDeviceConfirmed()
        {
            if (ConnectedDevice != null)
            {
                scan.Dispose();
                isScanning = false;
                MessagingCenter.Send(this, "ConnectedDevice", ConnectedDevice);
                MessagingCenter.Unsubscribe<SelectDeviceViewModel>(this, "ConnectedDevice");
            }
        }
    }
}
