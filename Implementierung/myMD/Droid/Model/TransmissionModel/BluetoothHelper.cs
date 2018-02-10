using nexus.protocols.ble;
using Xamarin.Forms;

[assembly: Dependency(typeof(myMD.Model.TransmissionModel.Droid.BluetoothHelper))]
namespace myMD.Model.TransmissionModel.Droid
{
    public class BluetoothHelper : IBluetoothHelper
    {
        private static readonly IBluetoothLowEnergyAdapter adapter = BluetoothLowEnergyAdapter.ObtainDefaultAdapter(Android.App.Application.Context);

        public IBluetoothLowEnergyAdapter Adapter => adapter;
    }
}