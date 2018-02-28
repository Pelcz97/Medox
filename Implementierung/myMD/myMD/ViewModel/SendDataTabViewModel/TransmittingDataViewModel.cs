using Xamarin.Forms.Internals;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using myMD.ViewModel.OverviewTabViewModel;
using nexus.protocols.ble;
using nexus.protocols.ble.gatt;
using System.Collections.Generic;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        IBleGattServerConnection ConnectedServer { get; set; }
        ObservableCollection<DoctorsLetterViewModel> LettersToSend { get; set; }
        IDisposable notify { get; set; }
        public int NumberOfFiles { get; set; }

        private int NumberOfReadCycles;
        private string CurrentFile;

        IList<String> FilesAsString;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            FilesAsString = new List<String>();
            ConnectedServer = ModelFacade.GetConnectedServer();

            FindCharacteristics();
            ReadFileFromServer();
            FilesAsString.ForEach(file => {
                Debug.WriteLine("File: " + file);
            });
        }

        public async void ReadFileFromServer(){
            GetReadCycles();

            for (int i = 0; i < NumberOfReadCycles; i++){
                await ReadFromServer();
            }
            FilesAsString.Add(CurrentFile);
        }

        public async void GetNumberOfFiles()
        {
            try
            {
                var value = await ConnectedServer.ReadCharacteristicValue(myMD_FileTransfer, FileCounterCharacteristic);
                NumberOfFiles = BitConverter.ToInt16(value, 0);

                Debug.WriteLine("NumberOfFiles: " + NumberOfFiles);
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async void GetReadCycles(){
            try
            {
                var value = await ConnectedServer.ReadCharacteristicValue(myMD_FileTransfer, myMDreadCycleCount);
                NumberOfReadCycles = BitConverter.ToInt16(value, 0);
                Debug.WriteLine("NumberOfReadCycles: " + NumberOfReadCycles);
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async void FindCharacteristics(){
            

            foreach (var guid in await ConnectedServer.ListServiceCharacteristics(myMD_FileTransfer))
            {
                Debug.WriteLine(guid);
            }

            try
            {
                var value = await ConnectedServer.ReadCharacteristicValue(myMD_FileTransfer, FileCounterCharacteristic);
                Debug.WriteLine("Länge: " + value.Length);
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            ListenForNotify();
            await ReadFromServer();
            notify.Dispose();
            
        }

        public async Task ReadFromServer(){
            try
            {
                var str = await ConnectedServer.ReadCharacteristicValue(myMD_FileTransfer, DataCharacteristic);
                String ergebnis = System.Text.Encoding.UTF8.GetString(str, 0, str.Length);
                CurrentFile += ergebnis;

            } catch (GattException ex){
                Debug.WriteLine(ex);
            }
        }

        public void ListenForNotify()
        {
            try
            {
                notify = ConnectedServer.NotifyCharacteristicValue(
                    myMD_FileTransfer,
                    DataCharacteristic,
                    bytes => { Debug.WriteLine("Got notified; " + bytes);});
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
