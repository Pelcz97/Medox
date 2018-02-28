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
using nexus.protocols.ble;
using nexus.protocols.ble.gatt;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        String resultString = "";
        IBleGattServerConnection ConnectedServer { get; set; }
        ObservableCollection<DoctorsLetterViewModel> LettersToSend { get; set; }

        public int NumberOfFiles { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            ConnectedServer = ModelFacade.GetConnectedServer();

            FindCharacteristics();

         }

        public async void FindCharacteristics(){
            

            foreach (var guid in await ConnectedServer.ListServiceCharacteristics(myMDserviceGuid1))
            {
                Debug.WriteLine(guid);
            }

            try
            {
                var value = await ConnectedServer.ReadCharacteristicValue(myMDserviceGuid1, myMDfileCount);
                Debug.WriteLine("Länge: " + value.Length);
                //Debug.WriteLine("Aktueller Wert: " + System.Text.Encoding.UTF8.G
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            await ReadFileFromServer();

            Debug.WriteLine(resultString);
        }

        public async Task ReadFileFromServer(){
            try
            {
                var str = await ConnectedServer.ReadCharacteristicValue(myMDserviceGuid1, myMDcharGuid1);
                String ergebnis = System.Text.Encoding.UTF8.GetString(str, 0, str.Length);
                resultString += ergebnis;

                var notifier = ConnectedServer.NotifyCharacteristicValue(
                    myMDserviceGuid1,
                    myMDcharGuid1,
                    async bytes => { 
                        await ReadFileFromServer(); 
                    });
            } catch (GattException ex){
                Debug.WriteLine(ex);
            }
        }
    }
}
