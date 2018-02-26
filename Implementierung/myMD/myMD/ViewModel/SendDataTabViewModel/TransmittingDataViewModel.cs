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

        List<IGattService> services;

        public int NumberOfFiles { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            TargetDevice = ModelFacade.GetConnected();
            services = new List<IGattService>();

            /*MessagingCenter.Subscribe<SelectDoctorsLettersViewModel, ObservableCollection<DoctorsLetterViewModel>>(this, "SelectedLetters", (sender, arg) => {
                LettersToSend = arg;
            });
            MessagingCenter.Subscribe<SelectDeviceViewModel, IScanResult>(this, "ConnectedDevice", (sender, arg) => {
                TargetDevice = arg;
                Debug.WriteLine("SelectedDevice : " + TargetDevice.AdvertisementData.LocalName);
            });*/



            TargetDevice.WhenServiceDiscovered().Subscribe(service =>
            {
                services.Add(service);
                Debug.WriteLine("Services: " + service);
                if (service.Uuid == myMDcharGuid1){
                    Debug.WriteLine("Found myMDCharGuid1");
                }
            });

        }
    }
}
