using Android.App;
using Android.Content;
using myMD.Droid;
using nexus.protocols.ble;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(myMD.Model.TransmissionModel.Droid.BluetoothHelper))]
namespace myMD.Model.TransmissionModel.Droid
{
    public class BluetoothHelper : IBluetoothHelper
    {
        private static IBluetoothLowEnergyAdapter adapter;

        private static readonly string NO_CONTEXT = "The required application context isn't ready yet.";

        public IBluetoothLowEnergyAdapter Adapter
        {
            get
            {
                if (adapter == null)
                {
                     adapter = BluetoothLowEnergyAdapter.ObtainDefaultAdapter(MainActivity.GetContext() ?? throw new InvalidOperationException(NO_CONTEXT));
                }
                return adapter;
            }
        }
    }
}