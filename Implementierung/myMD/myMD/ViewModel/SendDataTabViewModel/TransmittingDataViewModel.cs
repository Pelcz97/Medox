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

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        IScanResult TargetDevice { get; set; }
        ObservableCollection<DoctorsLetterViewModel> LettersToSend { get; set; }

        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            MessagingCenter.Subscribe<SelectDoctorsLettersViewModel, ObservableCollection<DoctorsLetterViewModel>>(this, "SelectedLetters", (sender, arg) => {
                LettersToSend = arg;
            });
            MessagingCenter.Subscribe<SelectDeviceViewModel, IScanResult>(this, "ConnectedDevice", (sender, arg) => {
                TargetDevice = arg;
                Debug.WriteLine("SelectedDevice : " + TargetDevice.AdvertisementData.LocalName);
            });
        }
    }
}
