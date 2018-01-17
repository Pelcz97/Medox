using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using ModelInterface.DataModelInterface;
using Plugin.BluetoothLE;
using Xamarin.Forms;

namespace myMD.ViewModel.SendDataTabViewModel
{
    public class SelectDeviceViewModel
    {
        
        public IAdapter BleAdapter;

        public IDisposable scan;
        public ICommand ScanForDevices_Clicked { get; private set; }
        public ObservableCollection<ScanResultViewModel> Devices { get; }

        public SelectDeviceViewModel()
        {
            this.Devices = new ObservableCollection<ScanResultViewModel>();

            this.ScanForDevices_Clicked = new Command((sender) =>
            {
                AdapterStatus status = CrossBleAdapter.Current.Status;

                CrossBleAdapter.Current.WhenStatusChanged().Subscribe(status2 => { 
                    Debug.WriteLine("Status changed");
                });

                if (status == AdapterStatus.PoweredOn)
                {
                    var scanner = CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
                    {
                        ScanResultViewModel test = new ScanResultViewModel();
                        test.Device = scanResult.Device;
                        if (test != null) { 
                            Devices.Add(test);
                            Debug.WriteLine(test.Device.Name);
                        }
                    });

                    scanner.Dispose();
                }
            });
        }
    }
}
