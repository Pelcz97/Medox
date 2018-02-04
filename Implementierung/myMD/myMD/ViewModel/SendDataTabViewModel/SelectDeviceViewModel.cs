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
    [Preserve(AllMembers = true)]
    public class SelectDeviceViewModel : OverallViewModel.OverallViewModel
    {
        public IAdapter BleAdapter { get; set; }
        public ObservableCollection<ScanResultViewModel> DeviceList { get; }
        private bool isScanning { get; set; }
        public IDisposable scan { get; set; }
        public ICommand ScanForDevices_Clicked { get => new Command(StartScan); }

        public SelectDeviceViewModel()
        {
            this.DeviceList = new ObservableCollection<ScanResultViewModel>();   
        }

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


            if (status == AdapterStatus.PoweredOn)
            {
                isScanning = true;
                this.scan = CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
                {
                    ScanResultViewModel test = new ScanResultViewModel();
                    test.Device = scanResult.Device;
                    if (test != null && DeviceList.All(x => x.Device != test.Device))
                    {
                        DeviceList.Add(test);
                        DeviceList.FirstOrDefault();
                        Debug.WriteLine("Device: " + test.Device);
                        Debug.WriteLine("Device.Name: " + test.Device.Name);
                        Debug.WriteLine("Device.UUID: " + test.Device.Uuid);
                    }
                });
            }
        }

        public void StopScan()
        {
            if (isScanning)
            {
                this.scan.Dispose();
            }
            isScanning = false;
        }

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
