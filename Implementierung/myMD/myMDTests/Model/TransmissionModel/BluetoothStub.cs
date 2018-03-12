using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using myMD.Model.TransmissionModel;
using nexus.protocols.ble;

namespace myMDTests.Model.TransmissionModel
{
    public class BluetoothStub : IBluetooth
    {
        public string File { get; private set; }

        public IBleGattServerConnection ConnectedGattServer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void cancelSend()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNumberOfFiles()
        {
            throw new NotImplementedException();
        }

        public int GetReadCycles(int FileNumber)
        {
            throw new NotImplementedException();
        }

        public byte[] ListToArray(List<byte[]> list)
        {
            throw new NotImplementedException();
        }

        public Task<List<List<byte[]>>> ReadAllFilesOnServer()
        {
            throw new NotImplementedException();
        }

        public Task ReadFileFromServer()
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ReadFromCharacteristic(Guid service, Guid characteristic)
        {
            throw new NotImplementedException();
        }

        public Task<List<byte[]>> ReadSpecificFile(int FileNumber)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> WriteToCharacteristic(Guid service, Guid characteristic, byte[] value)
        {
            throw new NotImplementedException();
        }

        Task<int> IBluetooth.GetReadCycles(int FileNumber)
        {
            throw new NotImplementedException();
        }

        Task<List<byte[]>> IBluetooth.ReadAllFilesOnServer()
        {
            throw new NotImplementedException();
        }
    }
}
