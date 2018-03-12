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

        /// <summary>
        /// Guids of the services and characteristics myMD is looking for on a server
        /// </summary>
        public static Guid myMD_FileTransfer = new Guid("88800000-8000-8000-8000-800000000000");
        public static Guid RequestAndRespond = new Guid("10000000-1000-1000-1000-100000000000");
        public static Guid RequestNumberOfSlices = new Guid("20000000-2000-2000-2000-200000000000");
        public static Guid RequestNumberOfFiles = new Guid("30000000-3000-3000-3000-300000000000");

        /// <summary>
        /// The connected device hosting the files
        /// </summary>
        /// <value>The connected gatt server.</value>
        public IBleGattServerConnection ConnectedGattServer { get; set; }

        /// <summary>
        /// Concates the seperate arrays inside a list into one and returns 
        /// a list of all received files, each as a single byte array
        /// </summary>
        /// <returns>The all files.</returns>
        /// <param name="files">Files.</param>
        public List<byte[]> FormatAllFiles(List<List<byte[]>> files)
        {
            List<byte[]> result = new List<byte[]>();

            foreach (List<byte[]> file in files)
            {
                result.Add(ListToArray(file));
            }

            return result;
        }


        /// <summary>
        /// Method to read all offered files on the ConnectedGattServer
        /// </summary>
        /// <returns>The all files on server.</returns>
        public async Task<List<byte[]>> ReadAllFilesOnServer()
        {

            int NumberOfFiles = await GetNumberOfFiles();
            Debug.WriteLine("Number of Files: " + NumberOfFiles);

            if (NumberOfFiles != 0)
            {
                List<List<byte[]>> ListOfFiles = new List<List<byte[]>>();

                for (int i = 0; i < NumberOfFiles; i++)
                {
                    var file = await ReadSpecificFile(i);

                    if (file != null)
                    {
                        ListOfFiles.Add(file);
                    }
                }

                return FormatAllFiles(ListOfFiles);

            }
            Debug.WriteLine("Server seems to be empty or something went wrong.");
            return null;
        }

        /// <summary>
        /// Read a specific file
        /// </summary>
        /// <returns>The specific file as a list of byte arrays.</returns>
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
            }
            Debug.WriteLine("The file you're looking for seems to be empty or not existend.");
            return null;
        }

        /// <summary>
        /// Requests the given File with the given number of times to read
        /// </summary>
        /// <returns>The specific file slice as byte array.</returns>
        /// <param name="FileNumber">File number.</param>
        /// <param name="NumberOfSlices">Number of slices.</param>
        public async Task<List<byte[]>> RequestFileSlice(int FileNumber, int NumberOfSlices)
        {
            try
            {
                List<byte[]> resultList = new List<byte[]>();

                for (int i = 0; i < NumberOfSlices; i++)
                {
                    byte[] request = Encoding.UTF8.GetBytes(FileNumber + "," + i);
                    //var write = ConnectedGattServer.WriteCharacteristicValue(myMD_FileTransfer, RequestAndRespond, request);
                    if (ConnectedGattServer != null)
                    {
                        var write = WriteToCharacteristic(myMD_FileTransfer, RequestAndRespond, request);

                        await Task.Delay(100);

                        //var read = ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestAndRespond);
                        var read = ReadFromCharacteristic(myMD_FileTransfer, RequestAndRespond);
                        resultList.Add(await read);
                    }
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
        /// The amount of files offered by the ConnectedGattServer
        /// </summary>
        /// <returns>The number of files.</returns>
        public async Task<int> GetNumberOfFiles()
        {
            try
            {
                if (ConnectedGattServer != null)
                {
                    /// The server answers a Read-Request with the number of files he's currently holding
                    var read = ReadFromCharacteristic(myMD_FileTransfer, RequestNumberOfFiles);
                    //var read = ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestNumberOfFiles);

                    if (await read != null)
                    {
                        return BitConverter.ToInt32(await read, 0);
                    }
                }
                return 0;
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
                if (ConnectedGattServer != null)
                {
                    //Convert FileNumber to byte array
                    byte[] request = Encoding.UTF8.GetBytes(FileNumber.ToString());

                    //Write the Number in the characteristic
                    var value = WriteToCharacteristic(myMD_FileTransfer, RequestNumberOfSlices, request);

                    await Task.Delay(100);

                    /// When a Read-Request is now send, the server will respond with 
                    /// the Number of times one has to read to receive all the parts of the wanted file
                    /// var read = ConnectedGattServer.ReadCharacteristicValue(myMD_FileTransfer, RequestNumberOfSlices);
                    var read = ReadFromCharacteristic(myMD_FileTransfer, RequestNumberOfSlices);
                    return BitConverter.ToInt32(await read, 0);
                }
                return 0;
            }
            catch (GattException ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// Method to read from a specific characteristic in a specific service.
        /// </summary>
        /// <returns>The result of the read.</returns>
        /// <param name="service">Service.</param>
        /// <param name="characteristic">Characteristic.</param>
        Task<byte[]> ReadFromCharacteristic(Guid service, Guid characteristic)
        {
            var read = ConnectedGattServer.ReadCharacteristicValue(service, characteristic);
            return read;
        }

        /// <summary>
        /// Method to write into a specific characteristic in a specific service.
        /// </summary>
        /// <returns>Nothing relevant.</returns>
        /// <param name="service">Service.</param>
        /// <param name="characteristic">Characteristic.</param>
        /// <param name="value">Value.</param>
        Task<byte[]> WriteToCharacteristic(Guid service, Guid characteristic, byte[] value)
        {
            var message = ConnectedGattServer.WriteCharacteristicValue(service, characteristic, value);
            return message;
        }

        /// <summary>
        /// Method to concate a list of arrays into one single array.
        /// </summary>
        /// <returns>The to array.</returns>
        /// <param name="list">List.</param>
        public byte[] ListToArray(List<byte[]> list)
        {
            var result = list.SelectMany(i => i).ToArray();
            return result;
        }
    }
}

