using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Linq;
using System.Threading.Tasks;
using nexus.protocols.ble.scan;
using nexus.core;
using nexus.protocols.ble.scan.advertisement;
using nexus.protocols.ble;

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
        public IBleGattServerConnection ConnectedServer { get; set; }

        public bool ConfirmingDevicePossible { get; set; }
        public bool CancelSelectDevicePossible { get; set; }

        /// <summary>
        /// Konstruktor für ein SelectDeviceViewModel
        /// </summary>
        public SelectDeviceViewModel()
        {
            this.DeviceList = new ObservableCollection<ScanResultViewModel>();
            ConfirmingDevicePossible = false;
            OnPropertyChanged("SavingPossible");
            CancelSelectDevicePossible = true;
            OnPropertyChanged("CancelSelectDevicePossible");

            if(BluetoothAdapter.AdapterCanBeEnabled){
                BluetoothAdapter.EnableAdapter();
            }

            if (BluetoothAdapter.CurrentState.Value == EnabledDisabledState.Enabled)
            {
                StartScan();
            } else {
                BluetoothAdapter.CurrentState.Subscribe(state =>
                {
                    
                    Debug.WriteLine("New state, " + state);
                    if (state == EnabledDisabledState.Enabled)
                    {
                        StartScan();
                    }
                });
            }
        }

        /// <summary>
        /// Methode um einen Scan zu starten.
        /// Dazu wird erst überprüft, ob gerade gescannt wird. Ist dies der Fall wird der aktive Scan abgebrochen.
        /// Dann wird geprüft ob Bluetooth für das Gerät verfügbar ist.
        /// Danach wird der Scan gestartet und die gefundenen Geräte in die Liste <see cref="T:myMD.ViewModel.SendDataTabViewModel.SelectDeviceLetterViewModel.DeviceList"/> gespeichert.
        /// </summary>
        public async void StartScan()
        {
            await BluetoothAdapter.ScanForBroadcasts(new ScanFilter()
                    .SetIgnoreRepeatBroadcasts(true)
                    .AddAdvertisedService(myMD_FileTransfer), peripheral =>
            {
                ScanResultViewModel test = new ScanResultViewModel(peripheral);

                Device.BeginInvokeOnMainThread(
                   () =>
                   {
                        DeviceList.Add(test);
                        DeviceList.FirstOrDefault();

                        var adv = peripheral.Advertisement;
                        Debug.WriteLine("### " + adv.DeviceName);
                        OnPropertyChanged("DeviceList");
                   });
            });
        }

        /// <summary>
        /// Methode um sich mit einem anderen Gerät zu verbinden
        /// </summary>
        /// <param name="item"></param>
        public async void ConnectToDevice(object item)
        {
            if(ConnectedServer != null){
                ConnectedServer.Dispose();
                ConnectedServer = null;
            }
            var ScanResultItem = (ScanResultViewModel)item;

            var connection = await BluetoothAdapter.ConnectToDevice(
            ScanResultItem.ScanResult,
            TimeSpan.FromSeconds(15));
            
            if (connection.IsSuccessful())
            {
                var gattServer = connection.GattServer;
                ConnectedServer = gattServer;
                ConfirmingDevicePossible = true;
                OnPropertyChanged("ConfirmingDevicePossible");

                gattServer.Subscribe(state =>
                {
                    if (state == ConnectionState.Disconnected)
                    {
                        ConfirmingDevicePossible = false;
                        OnPropertyChanged("ConfirmingDevicePossible");
                        Debug.WriteLine("Connection Lost");
                    }
                });
            }
            else
            {
                ConnectedServer = null;
                ConfirmingDevicePossible = false;
                OnPropertyChanged("ConfirmingDevicePossible");
                Debug.WriteLine("Error connecting to device. result={0:g}", connection.ConnectionResult);
            }
        }

        public void SetConnectedServer(){
            ModelFacade.SetConnectedServer(ConnectedServer);
            MessagingCenter.Send(this, "SetServer");
            MessagingCenter.Unsubscribe<SelectDeviceViewModel>(this, "SavedMedication");
        }
    }
}
