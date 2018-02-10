using nexus.protocols.ble;

namespace myMD.Model.TransmissionModel
{
    public interface IBluetoothHelper
    {
        IBluetoothLowEnergyAdapter Adapter { get; }
    }
}
