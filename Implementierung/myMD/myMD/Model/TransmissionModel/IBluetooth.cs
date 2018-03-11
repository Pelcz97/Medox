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
        IBleGattServerConnection ConnectedGattServer { get; set; }

        Task<List<byte[]>> ReadAllFilesOnServer();
        Task<List<byte[]>> ReadSpecificFile(int FileNumber);
        Task<int> GetReadCycles(int FileNumber);
        Task<int> GetNumberOfFiles();

        Task<byte[]> ReadFromCharacteristic(Guid service, Guid characteristic);
        Task<byte[]> WriteToCharacteristic(Guid service, Guid characteristic, byte[] value);

	}

}

