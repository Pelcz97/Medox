using myMD.ModelInterface.TransmissionModelInterface;
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

        Task ReadFileFromServer();
        void ReadFileZero();
        int GetReadCycles(int FileNumber);
	}

}

