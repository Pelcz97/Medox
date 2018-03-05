using Xamarin.Forms.Internals;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using myMD.ViewModel.OverviewTabViewModel;
using nexus.protocols.ble;
using nexus.protocols.ble.gatt;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        IBleGattServerConnection ConnectedGattServer { get; set; }

        public ICommand ReceiveData
        {
            get
            {
                return new Command(async (sender) =>
                {
                    await ModelFacade.GetFilesFromServer();
                });
            }
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            ConnectedGattServer = ModelFacade.GetConnectedServer();
        }

    }
}
