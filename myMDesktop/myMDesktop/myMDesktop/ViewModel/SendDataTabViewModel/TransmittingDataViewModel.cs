using Xamarin.Forms.Internals;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using myMD.ViewModel.OverallViewModel;
using Plugin.BluetoothLE;
using Plugin.BluetoothLE.Server;
using System.Text;
using System.Reactive.Linq;
using ReactiveUI;
using System.Collections.Generic;
using myMDesktop.Model.TransmissionModel;

namespace myMDesktop.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel
    {

        string output;
        public static Guid myMDguid = new Guid("00000000-1000-1000-1000-00805F9B0000");
        public static Guid myMDserviceGuid1 = new Guid("10000000-1000-1000-1000-100000000000");
        public static Guid myMDcharGuid1 = new Guid("30000000-3000-3000-3000-300000000000");
        
        public IServer serverTest { get; set; }
        

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {

            //serverTest = DependencyService.Get<IServer>();
            //serverTest.StartServer();

            StartServer();

            /*MessagingCenter.Subscribe<SelectDoctorsLettersViewModel, ObservableCollection<DoctorsLetterViewModel>>(this, "SelectedLetters", (sender, arg) => {
                LettersToSend = arg;
            });*/
            /*MessagingCenter.Subscribe<SelectDeviceViewModel, IBlePeripheral>(this, "ConnectedDevice", (sender, arg) => {
                 TargetDevice = arg;
                 Debug.WriteLine("SelectedDevice : " + TargetDevice.Advertisement.DeviceName);
             });*/
        }

        public async void StartServer()
        {
            await DependencyService.Get<IServer>().StartServer();
        }
    }
}
