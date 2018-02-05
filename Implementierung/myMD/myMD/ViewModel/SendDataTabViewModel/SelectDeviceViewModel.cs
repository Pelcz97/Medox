using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Plugin.BluetoothLE;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;


namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Diese Klasse übernimmt die Auswahl des Gerätes an das die Arztbriefe gesendet werden sollen. Dazu wird die Umgebung nach Geräten gescannt, die dann in  <see cref="T:myMD.ViewModel.SendDataTabViewModel.ScanResultLetterViewModel"/> modeliert werden.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SelectDeviceViewModel : OverallViewModel.OverallViewModel
    {
        //public IAdapter BleAdapter { get; set; }

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

        /// <summary>
        /// ICommand um das Antippen des Scan-Buttons zu handeln
        /// </summary>
        public ICommand ScanForDevices_Clicked { get => new Command(StartScan); }

        /// <summary>
        /// Konstruktor für ein SelectDeviceViewModel
        /// </summary>
        public SelectDeviceViewModel()
        {
            this.DeviceList = new ObservableCollection<ScanResultViewModel>();   
        }

        /// <summary>
        /// Methode um einen Scan zu starten.
        /// Dazu wird erst überprüft, ob gerade gescannt wird. Ist dies der Fall wird der aktive Scan abgebrochen.
        /// Dann wird geprüft ob Bluetooth für das Gerät verfügbar ist.
        /// Danach wird der Scan gestartet und die gefundenen Geräte in die Liste <see cref="T:myMD.ViewModel.SendDataTabViewModel.SelectDeviceLetterViewModel.DeviceList"/> gespeichert.
        /// </summary>
        public void StartScan(){
            if (isScanning == true)
            {
                scan.Dispose();
                DeviceList.Clear();
            }

            AdapterStatus status = CrossBleAdapter.Current.Status;

            CrossBleAdapter.Current.WhenStatusChanged().Subscribe(x =>
            {
                Debug.WriteLine(status);
            });

                isScanning = true;
                this.scan = CrossBleAdapter.Current.ScanWhenAdapterReady().Subscribe(scanResult =>
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
        public void ConnectToDevice(object item){
            var ScanResultItem = (ScanResultViewModel)item;
            IDevice device = ScanResultItem.Device;
            Debug.WriteLine("Gerät = " + device.Name);
            Debug.WriteLine("PairingStatus = " + device.PairingStatus);
            Debug.WriteLine("PairingPossible = " + device.IsPairingAvailable());
            ScanResultItem.Device.Connect();

        }
    }
}
