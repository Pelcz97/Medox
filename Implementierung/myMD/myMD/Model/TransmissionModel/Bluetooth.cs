using System.Collections.Generic;
using ModelInterface.TransmissionModelInterface;

namespace myMD.Model.TransmissionModel
{
    public class Bluetooth : IBluetooth
    {
        public void cancelSend()
        {
            throw new System.NotImplementedException();
        }

        public bool connect(IDevice device)
        {
            throw new System.NotImplementedException();
        }

        public bool pair(IDevice device, string pin)
        {
            throw new System.NotImplementedException();
        }

        public string receive()
        {
            throw new System.NotImplementedException();
        }

        public IList<IDevice> scanForDevices()
        {
            throw new System.NotImplementedException();
        }

        public void send(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }

}

