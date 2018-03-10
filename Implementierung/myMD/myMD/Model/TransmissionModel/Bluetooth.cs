using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static Guid RequestAndRespond = new Guid("10000000-1000-1000-1000-100000000000");
        public static Guid RequestNumberOfSlices = new Guid("20000000-2000-2000-2000-200000000000");
        public static Guid RequestNumberOfFiles = new Guid("30000000-3000-3000-3000-300000000000");

        public IBleGattServerConnection ConnectedGattServer { get; set; }

        /// <summary>
        /// Formats all files.
        /// </summary>
        /// <returns>The all files.</returns>
        /// <param name="files">Files.</param>
        public List<byte[]> FormatAllFiles(List<List<byte[]>> files){
            
            List<byte[]> result = new List<byte[]>();

            foreach (List<byte[]> file in files)
            {
                result.Add(ListToArray(file));
            }

            return result;
        }


        /// <summary>
        /// Reads all files on server.
        /// </summary>
        /// <returns>The all files on server.</returns>
        public async Task<List<byte[]>> ReadAllFilesOnServer(){
            
            int NumberOfFiles = await GetNumberOfFiles();
            Debug.WriteLine("Number of Files: " + NumberOfFiles);

            if(NumberOfFiles != 0){
                List<List<byte[]>> ListOfFiles = new List<List<byte[]>>();

                for (int i = 0; i < NumberOfFiles; i++){
                    var file = await ReadSpecificFile(i);

                    if(file != null){
                        ListOfFiles.Add(file);
                    }
                }

                return FormatAllFiles(ListOfFiles);

            } else {
                Debug.WriteLine("Server seems to be empty or something went wrong.");
                return null;
            }
        }

        /// <summary>
        /// Reads the specific file.
        /// </summary>
        /// <returns>The specific file.</returns>
        /// <param name="FileNumber">File number.</param>
        public async Task<List<byte[]>> ReadSpecificFile(int FileNumber)
        {
            int NumberOfSections = await GetReadCycles(FileNumber);
            Debug.WriteLine("Read Cycles: " + NumberOfSections);

            if (NumberOfSections != 0)
            {
                List<byte[]> FileAsBytes = new List<byte[]>();

                FileAsBytes = await RequestFileSlice(FileNumber, NumberOfSections);

                foreach (byte[] array in FileAsBytes)
                {
                    Debug.WriteLine(BitConverter.ToString(array));
                }

                return FileAsBytes;
            } else {
                Debug.WriteLine("The file you're looking for seems to be empty or not existend.");
                return null;
            }
        }

        /// <summary>
        /// Requests the given File with the given number of times to read
        /// </summary>
        /// <returns>The AF ile.</returns>
        /// <param name="NumberOfSlices">Number of slices.</param>
        public async Task<List<byte[]>> RequestFileSlice(int FileNumber, int NumberOfSlices)
        {
            try
            {
                List<byte[]> resultList = new List<byte[]>();

                for (int i = 0; i < NumberOfSlices; i++)
                {
                    byte[] request = Encoding.UTF8.GetBytes(FileNumber + "," + i);
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

        /// <summary>
        /// Gets the number of files.
        /// </summary>
        /// <returns>The number of files.</returns>
        public async Task<int> GetNumberOfFiles()
        {
            try
            {
                if (ConnectedGattServer != null)
                {
                    /// The server answers a Read-Request with the number of files he's currently holding
                    var read = ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestNumberOfFiles);

                    Debug.WriteLine(BitConverter.ToInt32(await read, 0));

                    return BitConverter.ToInt32(await read, 0);
                } else {
                    return 0;
                }
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// Get the number of requests the system has to send 
        /// to the server to receive all the parts of the wanted file
        /// </summary>
        /// <returns>The read cycles.</returns>
        /// <param name="FileNumber">File number.</param>
        public async Task<int> GetReadCycles(int FileNumber)
        {
            try
            {
                //Convert FileNumber to byte array
                byte[] request = Encoding.UTF8.GetBytes(FileNumber.ToString());

                //Write the Number in the characteristic
                var value = ConnectedGattServer.WriteCharacteristicValue(
                    myMD_FileTransfer,
                    RequestNumberOfSlices,
                    request);

                await Task.Delay(100);

                /// When a Read-Request is now send, the server will respond with 
                /// the Number of times one has to read to receive all the parts of the wanted file
                var read = ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestNumberOfSlices);

                return BitConverter.ToInt32(await read, 0);
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
        }


        private byte[] ListToArray(List<byte[]> list){
            
            var result = list.SelectMany(i => i).ToArray();
            return result;
        }
    }
}

