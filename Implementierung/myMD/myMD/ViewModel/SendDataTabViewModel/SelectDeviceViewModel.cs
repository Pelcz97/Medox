using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using myMD.ModelInterface.DataModelInterface;
using Plugin.BluetoothLE;
using Xamarin.Forms;
using myMD.ModelInterface.ModelFacadeInterface;

namespace myMD.ViewModel.SendDataTabViewModel
{
    public class SelectDeviceViewModel : OverallViewModel.OverallViewModel
    {
        public IAdapter BleAdapter;

        public IDisposable scan;
        public ICommand ScanForDevices_Clicked { get; private set; }
        public ObservableCollection<ScanResultViewModel> DeviceList { get; }

        public SelectDeviceViewModel()
        {
            this.DeviceList = new ObservableCollection<ScanResultViewModel>();
            this.ScanForDevices_Clicked = new Command((sender) =>
            {
                AdapterStatus status = CrossBleAdapter.Current.Status;

                CrossBleAdapter.Current.WhenStatusChanged().Subscribe(status2 =>
                {
                    Debug.WriteLine(status);
                });

                TimeSpan t = new TimeSpan(hours: 0, minutes: 0, seconds: 1);

                if (status == AdapterStatus.PoweredOn)
                {
                    this.scan = CrossBleAdapter.Current.ScanInterval(t).Subscribe(scanResult =>
                    {
                        ScanResultViewModel test = new ScanResultViewModel();
                        test.Device = scanResult.Device;
                        if (test != null)
                        {
                            DeviceList.Add(test);
                            Debug.WriteLine(test.Device.Name);

                        }
                    });
                    //scan.Dispose();
                    //scanner.Dispose();
                }
            });

                //

                /*if (status == AdapterStatus.PoweredOn)
                {
                        var scanner = this.BleAdapter.Scan().Subscribe(scanResult =>
                        {
                            ScanResultViewModel test = new ScanResultViewModel();
                            test.Device = scanResult.Device;
                            if (test != null) { 
                                DeviceList.Add(test);
                                Debug.WriteLine(test.Device.Name);
                            }
                        });

                        scanner.Dispose();
                    }
                });*/
        }
    }
}
