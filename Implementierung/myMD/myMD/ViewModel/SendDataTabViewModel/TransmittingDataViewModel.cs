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
        IDisposable notifier;
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            ConnectedGattServer = ModelFacade.GetConnectedServer();
            var res = GetReadCycles(0);
            Debug.WriteLine("Ergebnis: " + res);

        }


        public int GetReadCycles(int FileNumber)
        {
            int number = 0;
            try
            {
                
                notifier = ConnectedGattServer.NotifyCharacteristicValue(
                        myMD_FileTransfer,
                        myMDReadCycleCount,

                        bytes =>
                        {
                            Debug.WriteLine("Serverantwort: " + BitConverter.ToString(bytes));
                            number = BitConverter.ToInt32(bytes, 0);
                        });
                byte[] request = Encoding.UTF8.GetBytes(FileNumber.ToString());

                Task.WhenAll(new Task[] {
                    ConnectedGattServer.WriteCharacteristicValue(
                        myMD_FileTransfer,
                        myMDReadCycleCount,
                        request)
                    });

            } catch (GattException ex){
                Debug.WriteLine(ex);
                return 0;
            }
            return number;
        }
    }
}
