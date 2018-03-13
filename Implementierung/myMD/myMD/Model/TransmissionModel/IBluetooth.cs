using System.Collections.Generic;
using nexus.protocols.ble;
using System.Threading.Tasks;
using System;

namespace myMD.Model.TransmissionModel
{
    /// <summary>
    ///  Schnittstelle zur Bluetoothuebertragung
    /// </summary>
	public interface IBluetooth
	{
        /// <summary>
        /// Gets or sets the connected gatt server.
        /// </summary>
        /// <value>The connected gatt server.</value>
        IBleGattServerConnection ConnectedGattServer { get; set; }

        /// <summary>
        /// Reads all files on server.
        /// </summary>
        /// <returns>A List of all the files as byte arrays.</returns>
        Task<List<byte[]>> ReadAllFilesOnServer();

        /// <summary>
        /// Reads a specific file.
        /// </summary>
        /// <returns>The file index.</returns>
        /// <param name="FileNumber">File number.</param>
        Task<List<byte[]>> ReadSpecificFile(int FileNumber);

        /// <summary>
        /// Gets the number of read cycles.
        /// </summary>
        /// <returns>The read cycles.</returns>
        /// <param name="FileNumber">File number.</param>
        Task<int> GetReadCycles(int FileNumber);

        /// <summary>
        /// Gets the number of files on the server.
        /// </summary>
        /// <returns>The number of files.</returns>
        Task<int> GetNumberOfFiles();

        /// <summary>
        /// Lists to array.
        /// </summary>
        /// <returns>The to array.</returns>
        /// <param name="list">List.</param>
        byte[] ListToArray(List<byte[]> list);
	}

}

