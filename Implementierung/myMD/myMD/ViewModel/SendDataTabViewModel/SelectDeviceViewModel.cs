using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using System.Threading.Tasks;
using Plugin.BluetoothLE;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Diese Klasse übernimmt die Auswahl des Gerätes an das die Arztbriefe gesendet werden sollen. Dazu wird die Umgebung nach Geräten gescannt, die dann in  <see cref="T:myMD.ViewModel.SendDataTabViewModel.ScanResultLetterViewModel"/> modeliert werden.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SelectDeviceViewModel : OverallViewModel.OverallViewModel
    {
        //public IAdapter BleAdapter { get; set; }
        public static Guid myMDguid = new Guid("00000000-1000-1000-1000-00805F9B0000");
        public static Guid myMDserviceGuid1 = new Guid("10000000-1000-1000-1000-100000000000");
        public static Guid myMDserviceGuid2 = new Guid("20000000-2000-2000-2000-200000000000");
        public static Guid myMDcharGuid1 = new Guid("30000000-3000-3000-3000-300000000000");
        public static Guid myMDcharGuid2 = new Guid("40000000-4000-4000-4000-400000000000");

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

            AdapterStatus status = CrossBleAdapter.Current.Status;

            CrossBleAdapter.Current.WhenStatusChanged().Subscribe(x => {
                Debug.WriteLine(status);
            });

            isScanning = true;


            this.scan = CrossBleAdapter.Current.Scan(new ScanConfig { 
                ScanType = BleScanType.Balanced, 
                ServiceUuid = myMDserviceGuid1
            }).Subscribe(scanResult =>
            {
                ScanResultViewModel test = new ScanResultViewModel();
                test.Device = scanResult.Device;
                if (test != null && DeviceList.All(x => x.Device != test.Device) && test.Device.Name != null)
                {
                    DeviceList.Add(test);
                    DeviceList.FirstOrDefault();
                    Debug.WriteLine("Device: " + test.Device);
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

            Debug.WriteLine("Gerät = " + device.Name);
            try {
                device.Connect();
                
                if (device.IsPairingAvailable() && device.PairingStatus != PairingStatus.Paired)
                {
                    // there is an optional argument to pass a PIN in PairRequest as well
                    device.PairingRequest("1111").Subscribe(isSuccessful => { Debug.WriteLine("Successfully paired"); });
                }

            } 
            catch (Exception ex){
                Debug.WriteLine(ex);
            }


        }



        public void TargetDeviceConfirmed()
        {
            if (ConnectedDevice != null)
            {
                MessagingCenter.Send(this, "ConnectedDevice", ConnectedDevice);
                MessagingCenter.Unsubscribe<SelectDeviceViewModel>(this, "ConnectedDevice");
            }
        }
    }
}
