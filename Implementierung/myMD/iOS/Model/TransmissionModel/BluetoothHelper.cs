﻿using nexus.protocols.ble;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(myMD.Model.TransmissionModel.iOS.BluetoothHelper))]
namespace myMD.Model.TransmissionModel.iOS
{
    [Preserve(AllMembers = true)]
    public class BluetoothHelper : IBluetoothHelper
    {
        private static readonly IBluetoothLowEnergyAdapter adapter = BluetoothLowEnergyAdapter.ObtainDefaultAdapter();

        public IBluetoothLowEnergyAdapter Adapter => adapter;
    }
}