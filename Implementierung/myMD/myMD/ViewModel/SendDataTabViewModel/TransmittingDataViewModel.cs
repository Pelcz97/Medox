using Xamarin.Forms.Internals;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using myMD.ViewModel.OverviewTabViewModel;
using Plugin.BluetoothLE;
using System.Collections.Generic;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        IDevice TargetDevice { get; set; }
        ObservableCollection<DoctorsLetterViewModel> LettersToSend { get; set; }

        List<IGattCharacteristic> services;

        IGattCharacteristic FileCounter { get; set; }
        IGattCharacteristic ReadCharacteristic { get; set; }
        IGattCharacteristic NotifyCharacteristic { get; set; }

        public int NumberOfFiles { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            TargetDevice = ModelFacade.GetConnected();
            var test = CrossBleAdapter.Current.GetConnectedDevices();
            services = new List<IGattCharacteristic>();

            TargetDevice.WhenServiceDiscovered().Subscribe(service =>
            {
                var chars = service.GetKnownCharacteristics();
                Debug.WriteLine(chars);
            });

            TargetDevice.WhenAnyCharacteristicDiscovered().Subscribe(service =>
            {
                Debug.WriteLine(service);
            });

            TargetDevice.WhenAnyDescriptorDiscovered().Subscribe(service =>
            {
                Debug.WriteLine("Descriptor " + service);
            });

            if (FileCounter != null)
            {
                FileCounter.ReadInterval(new TimeSpan(0, 0, 0, 0, 100)).Subscribe(result => { Debug.WriteLine("Current value: " + result); });
            }
         }
    }
}
