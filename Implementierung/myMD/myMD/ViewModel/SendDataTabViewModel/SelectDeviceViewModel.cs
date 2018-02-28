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

        /// <summary>
        /// Boolean, ob das Gerät des Nutzers gerade die Umbegung nach Geräten durchsucht
        /// </summary>
        private bool isScanning { get => CrossBleAdapter.Current.IsScanning; }


        public IBleGattServerConnection ConnectedServer { get => ModelFacade.GetConnectedServer(); }


        /// <summary>
        /// Konstruktor für ein SelectDeviceViewModel
        /// </summary>
        public SelectDeviceViewModel()
        {
            this.DeviceList = new ObservableCollection<ScanResultViewModel>();

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
            Debug.WriteLine("möp");

            await BluetoothAdapter.ScanForBroadcasts(new ScanFilter().SetAdvertisedManufacturerCompanyId()
                    .SetIgnoreRepeatBroadcasts(true), peripheral =>
            {
                ScanResultViewModel test = new ScanResultViewModel(peripheral);

                Device.BeginInvokeOnMainThread(
                   () =>
                   {
                        DeviceList.Add(test);
                        DeviceList.FirstOrDefault();

                        var adv = peripheral.Advertisement;
                        Debug.WriteLine("### " + adv.DeviceName);
                        Debug.WriteLine("### " + adv.Services.Select(x => x.ToString()).Join(","));
                        Debug.WriteLine("### " + adv.ManufacturerSpecificData.FirstOrDefault().CompanyName());
                        Debug.WriteLine("### " + adv.ServiceData);
                   });
            });

            /*CrossBleAdapter.Current.ScanWhenAdapterReady().Subscribe(scanResult =>
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
            });*/
        }



        public bool Connected { get; set; }

        /// <summary>
        /// Methode um sich mit einem anderen Gerät zu verbinden
        /// </summary>
        /// <param name="item"></param>
        public async Task ConnectToDevice(object item)
        {
            var ScanResultItem = (ScanResultViewModel)item;


            var connection = await BluetoothAdapter.ConnectToDevice(
            ScanResultItem.ScanResult,
            TimeSpan.FromSeconds(15));
            if (connection.IsSuccessful())
            {
                Debug.WriteLine("Success");
                var gattServer = connection.GattServer;
                ModelFacade.SetConnectedServer(gattServer);
            }
            else
            {
                Debug.WriteLine("Error connecting to device. result={0:g}", connection.ConnectionResult);
            }



            //IDevice device = ScanResultItem.Device;

            /*
            if (ScanResultItem.ScanResult.AdvertisementData.IsConnectable)
            {
                Debug.WriteLine("Connectable");
            }

            ScanResultItem.ScanResult.Device.WhenStatusChanged().Subscribe(connectionState =>
            {
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
            }*/
        }
    }
}
