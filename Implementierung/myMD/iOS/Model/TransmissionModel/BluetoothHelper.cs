using nexus.protocols.ble;
using Xamarin.Forms;

[assembly: Dependency(typeof(myMD.Model.TransmissionModel.iOS.BluetoothHelper))]
namespace myMD.Model.TransmissionModel.iOS
{
    public class BluetoothHelper : IBluetoothHelper
    {
        private static readonly IBluetoothLowEnergyAdapter adapter = BluetoothLowEnergyAdapter.ObtainDefaultAdapter();

        public IBluetoothLowEnergyAdapter Adapter => adapter;
    }
}