using System;
using Plugin.BluetoothLE;

namespace myMD.ViewModel.SendDataTabViewModel
{
    public class ScanResultViewModel
    {
        public IDevice Device { get; set; }
        /*
        string name;
        public string Name {get => this.Device.Name; set => Device.Name = value;}

        bool connected;
        public bool IsConnected { get; private set; }

        Guid uuid;
        public Guid Uuid{ get; private set; }

        string localName;
        public string LocalName { get; private set; }*/
    }
}
