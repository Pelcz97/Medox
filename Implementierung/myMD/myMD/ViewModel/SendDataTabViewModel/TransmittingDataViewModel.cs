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
using Acr.UserDialogs;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        IUserDialogs userDialogs;

        public bool ReadingDataPossible { get; set; }
        public bool SearchingPossible { get; set; }
        public bool ReadingNumberOfFilesPossible { get; set; }

        IBleGattServerConnection ConnectedGattServer { get {
                var server = ModelFacade.GetConnectedServer();
                ReadingDataPossible = server != null;
                OnPropertyChanged("ReadingDataPossible");

                server.Subscribe(state =>
                {
                    if (state == ConnectionState.Disconnected)
                    {
                        ReadingDataPossible = false;
                        OnPropertyChanged("ReadingDataPossible");
                        Debug.WriteLine("Connection Lost");
                    }
                });

                return server;
            } 
        }



        public ICommand ReceiveData
        {
            get
            {
                return new Command(async (sender) =>
                {
                    SearchingPossible = false;
                    ReadingDataPossible = false;
                    ReadingNumberOfFilesPossible = false;
                    OnPropertyChanged("SearchingPossible");
                    OnPropertyChanged("ReadingDataPossible");
                    OnPropertyChanged("ReadingNumberOfFilesPossible");
                    try{
                        await ModelFacade.GetFilesFromServer();
                    } catch (GattException ex){
                        Debug.WriteLine("Fucked up. " + ex);
                    } catch (InvalidOperationException invalidOperationEx) {
                        Debug.WriteLine("Falsche oder beschädigte Datei. " + invalidOperationEx);


                        userDialogs.Toast(new ToastConfig("Test Message")
                        //.SetBackgroundColor(bgColor)
                        //.SetMessageTextColor(msgColor)
                        .SetDuration(TimeSpan.FromSeconds(20))
                                          //.SetIcon(icon)
                                         );
                        
                    }

                    SearchingPossible = true;
                    ReadingDataPossible = true;
                    ReadingNumberOfFilesPossible = true;
                    OnPropertyChanged("SearchingPossible");
                    OnPropertyChanged("ReadingDataPossible");
                    OnPropertyChanged("ReadingNumberOfFilesPossible");

                    MessagingCenter.Send(this, "UpdateDoctorsLettersList");
                    MessagingCenter.Unsubscribe<TransmittingDataViewModel>(this, "UpdateDoctorsLettersList");
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
            userDialogs = UserDialogs.Instance;

            ReadingDataPossible = false;
            SearchingPossible = true;

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
