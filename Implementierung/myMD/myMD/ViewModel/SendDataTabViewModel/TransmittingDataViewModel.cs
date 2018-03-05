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

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        IBleGattServerConnection ConnectedGattServer { get; set; }
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            ConnectedGattServer = ModelFacade.GetConnectedServer();

            ModelFacade.GetFilesFromServer();
        }

       /* public async void GetReadCycles(int FileNumber)
        {
            try
            {
                
                var notifier = ConnectedGattServer.NotifyCharacteristicValue(
                        myMD_FileTransfer,
                        myMDReadCycleCount,
                        bytes =>
                        {
                            Debug.WriteLine("Serverantwort: " + BitConverter.ToString(bytes));
                            //number = BitConverter.ToInt32(bytes, 0);
                        });

                byte[] request = Encoding.UTF8.GetBytes(FileNumber.ToString());

                ConnectedGattServer.WriteCharacteristicValue(
                        myMD_FileTransfer,
                        myMDReadCycleCount,
                        request);

                await Task.Delay(1000);
                

            } catch (GattException ex){
                Debug.WriteLine(ex);
            }

        }*/

        public async void AlternativeReadWrite()
        {
            try
            {
                for (int i = 0; i <= 2; i++)
                {
                    byte[] request = Encoding.UTF8.GetBytes("0," + i);
                    var write = ConnectedGattServer.WriteCharacteristicValue(myMD_FileTransfer, RequestAndRespond, request);
                    await Task.Delay(100);
                    var read = ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestAndRespond);
                }
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
