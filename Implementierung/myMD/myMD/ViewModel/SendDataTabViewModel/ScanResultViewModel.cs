using System;
using Plugin.BluetoothLE;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.SendDataTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ScanResultViewModel
    {
        public IDevice Device { get; set; }

        public string DeviceName {get => this.Device.Name;}

        /*
        bool connected;
        public bool IsConnected { get; private set; }

        Guid uuid;
        public Guid Uuid{ get; private set; }

        string localName;
        public string LocalName { get; private set; }*/
    }
}
