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
        IBleGattServerConnection ConnectedGattServer { get {
                var server = ModelFacade.GetConnectedServer();
                ReadingDataPossible = server != null;
                OnPropertyChanged("ReadingDataPossible");
                return server;
            } 
        }

        public bool ReadingDataPossible { get; set; }

        public ICommand ReceiveData
        {
            get
            {
                return new Command(async (sender) =>
                {
                    ReadingDataPossible = false;
                    OnPropertyChanged("ReadingDataPossible");
                    await ModelFacade.GetFilesFromServer();
                    ReadingDataPossible = true;
                    OnPropertyChanged("ReadingDataPossible");
                });
            }
        }

        public ICommand RefreshNumberOfFiles
        {
            get
            {
                return new Command(async (sender) =>
                {
                    NumberOfFiles = await ModelFacade.NumberOfFilesOnServer();
                    OnPropertyChanged("NumberOfFiles");
                    if (NumberOfFiles > 0){
                        ReadingDataPossible = true;
                        OnPropertyChanged("ReadingDataPossible");
                    } else {
                        ReadingDataPossible = false;
                        OnPropertyChanged("ReadingDataPossible");
                    }
                });
            }
        }

        public int NumberOfFiles { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            ReadingDataPossible = false;

            MessagingCenter.Subscribe<SelectDeviceViewModel>(this, "SetServer", sender => {
                GetNumberOfFiles();
            });
        }

        private async void GetNumberOfFiles(){
            NumberOfFiles = await ModelFacade.NumberOfFilesOnServer();
            OnPropertyChanged("NumberOfFiles");
            if (NumberOfFiles > 0)
            {
                ReadingDataPossible = true;
                OnPropertyChanged("ReadingDataPossible");
            }
            else
            {
                ReadingDataPossible = false;
                OnPropertyChanged("ReadingDataPossible");
            }
        }

    }
}
