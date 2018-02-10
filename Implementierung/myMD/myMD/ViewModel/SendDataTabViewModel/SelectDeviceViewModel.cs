using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using nexus.protocols.ble;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using nexus.protocols.ble.scan;

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
        /// ICommand um das Antippen des Scan-Buttons zu handeln
        /// </summary>
        public ICommand ScanForDevices_Clicked { get => new Command(StartScan); }

        public IBlePeripheral ConnectedDevice { get; set; }

        /// <summary>
        /// Konstruktor für ein SelectDeviceViewModel
        /// </summary>
        public SelectDeviceViewModel()
        {
            ConnectedDevice = null;

            if (BluetoothAdapter.AdapterCanBeEnabled && BluetoothAdapter.CurrentState.IsDisabledOrDisabling())
            {
                BluetoothAdapter.EnableAdapter();
            }

            BluetoothAdapter.CurrentState.Subscribe(state => Debug.WriteLine("New State: {0}", state));

            this.DeviceList = new ObservableCollection<ScanResultViewModel>();
        }

        /// <summary>
        /// Methode um einen Scan zu starten.
        /// Dazu wird erst überprüft, ob gerade gescannt wird. Ist dies der Fall wird der aktive Scan abgebrochen.
        /// Dann wird geprüft ob Bluetooth für das Gerät verfügbar ist.
        /// Danach wird der Scan gestartet und die gefundenen Geräte in die Liste <see cref="T:myMD.ViewModel.SendDataTabViewModel.SelectDeviceLetterViewModel.DeviceList"/> gespeichert.
        /// </summary>
        public async void StartScan(){

            await BluetoothAdapter.ScanForBroadcasts(peripheral =>
            {
                Device.BeginInvokeOnMainThread(
                   () =>
                   {
                       ScanResultViewModel test = new ScanResultViewModel();
                       test.Device = peripheral;

                    if (peripheral != null && DeviceList.All(x => x.Device.DeviceId != test.Device.DeviceId)){
                        DeviceList.Add(test);
                    }

                   });
            });
        }

        /// <summary>
        /// Methode um sich mit einem anderen Gerät zu verbinden
        /// </summary>
        /// <param name="item"></param>
        public async void ConnectToDevice(object item){
            var ScanResultItem = (ScanResultViewModel)item;
            IBlePeripheral device = ScanResultItem.Device;

            Debug.WriteLine("Gerät = " + device.Advertisement.DeviceName);

            var connection = await BluetoothAdapter.ConnectToDevice(device, 
                                TimeSpan.FromSeconds(15),
                                progress => Debug.WriteLine(progress));

            if (connection.IsSuccessful())
            {
                ConnectedDevice = device;
            }
            else
            {
                Debug.WriteLine("Connecting failed");
            }
        }
    }
}
