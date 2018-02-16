using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using nexus.protocols.ble;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using nexus.protocols.ble.scan;
using System.Threading.Tasks;
using nexus.protocols.ble.gatt;
using nexus.protocols.ble.gatt.adopted;

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

        /// <summary>
        /// Liste an Geräten die in der Umgebung gefunden wurden
        /// </summary>
        public ObservableCollection<ScanResultViewModel> DeviceList { get; }

        public IBlePeripheral ConnectedDevice { get; set; }

        public IBleGattServerConnection serverConnection;

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

            BluetoothAdapter.CurrentState.Subscribe(state => {
                Debug.WriteLine("New State: {0}", state);
                if (state == EnabledDisabledState.Enabled){
                    StartScan();
                }
            });

            this.DeviceList = new ObservableCollection<ScanResultViewModel>();
        }

        /// <summary>
        /// Methode um einen Scan zu starten.
        /// Dazu wird erst überprüft, ob gerade gescannt wird. Ist dies der Fall wird der aktive Scan abgebrochen.
        /// Dann wird geprüft ob Bluetooth für das Gerät verfügbar ist.
        /// Danach wird der Scan gestartet und die gefundenen Geräte in die Liste <see cref="T:myMD.ViewModel.SendDataTabViewModel.SelectDeviceLetterViewModel.DeviceList"/> gespeichert.
        /// </summary>
        public async void StartScan(){

            await BluetoothAdapter.ScanForBroadcasts(new ScanFilter().AddAdvertisedService(myMDserviceGuid1), peripheral =>
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
                using (serverConnection = connection.GattServer)
                    try
                    {
                        var known = new KnownAttributes();

                        //Guid like: xxxxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx
                        Guid TestService = myMDguid;
                        /*Guid TestCharacteristic = new Guid("1234567899-2222-2222-2222-1234567899");
                        Guid TestDescriptor = new Guid("1234567899-3333-3333-3333-1234567899");

                        // You can add descriptions for any desired
                        // characteristics, services, and descriptors
                        known.AddService(TestService, "Foo");
                        known.AddCharacteristic(TestCharacteristic, "Bar");
                        known.AddDescriptor(TestDescriptor, "Baz");

                        // There are shortcuts to add all the attributes
                        // that have been adopted by the Bluetooth SIG
                        known.AddAdoptedServices();
                        known.AddAdoptedCharacteristics();
                        known.AddAdoptedDescriptors();*/

                        // You can also create a new KnownAttributes with all
                        // the above adopted attributes already populated:
                        known = KnownAttributes.CreateWithAdoptedAttributes();

                        // The resulting value of the characteristic is returned. In nearly all cases this
                        // will be the same value that was provided to the write call (e.g. `byte[]{ 1, 2, 3 }`)
                        /*var value = await serverConnection.WriteCharacteristicValue(
                        TestService,
                        TestCharacteristic,
                        new byte[] { 1, 2, 3 });*/
                    }
                    catch (GattException ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
            }
            else
            {
                Debug.WriteLine("Connecting failed");
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
