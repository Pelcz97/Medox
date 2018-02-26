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
        
        public IServer serverTest { get; set; }
        

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            
            StartServer();

        }

        public async void StartServer()
        {
            await DependencyService.Get<IServer>().StartServer();
        }
    }
}
