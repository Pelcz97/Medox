using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using myMD.ModelInterface.TransmissionModelInterface;
using nexus.protocols.ble;
using nexus.protocols.ble.gatt;
using Xamarin.Forms.Internals;

namespace myMD.Model.TransmissionModel
{
    /// <summary>
    /// Klasse um alle Bluetoothvorgänge zu kapseln
    /// </summary>
    public class Bluetooth : IBluetooth
    {
        public static Guid myMD_FileTransfer = new Guid("88800000-8000-8000-8000-800000000000");
        public static Guid DataCharacteristic = new Guid("50000000-5000-5000-5000-500000000000");
        public static Guid FileCounterCharacteristic = new Guid("40000000-4000-4000-4000-400000000000");
        public static Guid ReadResponse = new Guid("60000000-6000-6000-6000-600000000000");
        public static Guid myMDReadCycleCount = new Guid("70000000-7000-7000-7000-700000000000");
        public static Guid RequestAndRespond = new Guid("10000000-1000-1000-1000-100000000000");

        public IBleGattServerConnection ConnectedGattServer { get; set; }
        IDisposable ReadCycleNotify { get; set; }
        IDisposable Notify { get; set; }

        int NumberOfFiles { get; set; }
        int NumberOfReadCycles { get; set; }
        string CurrentFile { get; set; }

        IList<String> FilesAsString;

        public Bluetooth()
        {

            FilesAsString = new List<String>();

        }

        public async Task<List<byte[]>> RequestAFile(int NumberOfSlices)
        {
            try
            {
                List<byte[]> resultList = new List<byte[]>();

                for (int i = 0; i < NumberOfSlices; i++)
                {
                    byte[] request = Encoding.UTF8.GetBytes("0," + i);
                    var write = ConnectedGattServer.WriteCharacteristicValue(myMD_FileTransfer, RequestAndRespond, request);
                    await Task.Delay(100);
                    var read = ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestAndRespond);
                    resultList.Add(await read);
                }
                return resultList;
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public string ListToString(List<byte[]> list)
        {
            StringBuilder result = new StringBuilder();
            foreach (byte[] file in list)
            {
                result.Append(Encoding.UTF8.GetString(file, 0, file.Length)));
            }
            return result.ToString();
        }

        public async void ReadFileZero(){
            
            List<byte[]> fileAsList = await RequestAFile(14);
            foreach(byte[] file in fileAsList){
                Debug.WriteLine(Encoding.UTF8.GetString(file, 0, file.Length));
            }
        }

        public async Task<List<byte[]>> ReadSpecificFile(int FileNumber){
            List<byte[]> FileAsBytes = new List<byte[]>();

            int NumberOfSections = GetReadCycles(FileNumber);
            Debug.WriteLine("NumberOfSections: " + NumberOfSections);

            for (int i = 0; i <= NumberOfSections; i++){
                FileAsBytes.Add(await GetFileSection(FileNumber, i));
                Debug.WriteLine("Progress! " + i);   
            }

            return FileAsBytes;
        }

        public async Task<byte[]> GetFileSection(int FileNumber, int SectionNumber){
            try
            {
                byte[] request = Encoding.UTF8.GetBytes(FileNumber + "," + SectionNumber);
                var value = await ConnectedGattServer.WriteCharacteristicValue(
                    myMD_FileTransfer,
                    ReadResponse,
                    request);
                
                Debug.WriteLine("Empfangenes Byte[]: " + value);
                return value;
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        async Task<int> GetNumberOfFiles()
        {
            try
            {
                var value = await ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, FileCounterCharacteristic);
                NumberOfFiles = BitConverter.ToInt32(value, 0);

                Debug.WriteLine("NumberOfFiles: " + NumberOfFiles);
                return NumberOfFiles;
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
        }

        public int GetReadCycles(int FileNumber)
        {
            try
            {
                int number = 0;
                var notifier = ConnectedGattServer.NotifyCharacteristicValue(
                        myMD_FileTransfer,
                        myMDReadCycleCount,
                        bytes =>
                        {
                            Debug.WriteLine("Serverantwort: " + BitConverter.ToString(bytes));
                            number = BitConverter.ToInt32(bytes, 0);
                        }) as Task<IDisposable>;

                byte[] request = Encoding.UTF8.GetBytes(FileNumber.ToString());

                Task.WaitAll(new Task[] {
                    ConnectedGattServer.WriteCharacteristicValue(
                        myMD_FileTransfer,
                        myMDReadCycleCount,
                        request)
                    });

                Task.Delay(1000).Wait();

                Debug.WriteLine("Ergebnis: " + number);
                return number;

            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex);
                return 0;
            }
            catch (AggregateException ex){
                Debug.WriteLine(ex);
                return 0;
            }
        }

        public void ListenForNotify()
        {
            try
            {
                Notify = ConnectedGattServer.NotifyCharacteristicValue(
                    myMD_FileTransfer,
                    DataCharacteristic,
                    async bytes =>
                    {
                        await ReadFileFromServer();
                        await WriteValueToCharacteristic(myMD_FileTransfer, ReadResponse, new byte[] { 1 });
                    });
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        async Task<string> ReadCurrentSectionFromServer()
        {
            try
            {
                var str = await ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, DataCharacteristic);
                String result = System.Text.Encoding.UTF8.GetString(str, 0, str.Length);
                return result;
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        async Task WriteValueToCharacteristic(Guid ServiceGuid, Guid CharacteristicUuid, byte[] x)
        {
            try
            {
                var value = await ConnectedGattServer.WriteCharacteristicValue(
                    ServiceGuid,
                    CharacteristicUuid,
                    x);
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async Task ReadFileFromServer()
        {
            CurrentFile += await ReadCurrentSectionFromServer(); //Attention! can return Null, not handled yet
        }

    }
}

