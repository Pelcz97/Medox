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
            //ModelFacade.GetFilesFromServer();
            //GetReadCycles(0);
            //Debug.WriteLine(t);
            ReadOnly();
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
                byte[] request = Encoding.UTF8.GetBytes("0,0");

                /*var notifier = ConnectedGattServer.NotifyCharacteristicValue(
                        myMD_FileTransfer,
                        RequestAndRespond,
                        bytes =>
                        {
                            Debug.WriteLine("Serverantwort: " + BitConverter.ToString(bytes));
                        });*/

                var read = ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestAndRespond);

                await Task.WhenAll(new Task[]
                {

                    ConnectedGattServer.WriteCharacteristicValue(
                        myMD_FileTransfer, 
                        RequestAndRespond, 
                        request)
                });

                byte[] test = await read;
                Debug.WriteLine("Test array: " + test.ToString());
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async void ReadOnly(){
            var read = await ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestAndRespond);
            Debug.WriteLine("Test array: " + read.ToString());
        }
    }
}
