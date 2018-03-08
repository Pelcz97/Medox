using System.Collections.Generic;
using nexus.protocols.ble;
using System.Threading.Tasks;

namespace myMD.Model.TransmissionModel
{
    /// <summary>
    ///  Schnittstelle zur Bluetoothuebertragung
    /// </summary>
	public interface IBluetooth
	{
        IBleGattServerConnection ConnectedGattServer { get; set; }

        Task<List<List<byte[]>>> ReadAllFilesOnServer();
        Task<List<byte[]>> ReadSpecificFile(int FileNumber);
        Task<int> GetReadCycles(int FileNumber);
        Task<int> GetNumberOfFiles();
	}

}

