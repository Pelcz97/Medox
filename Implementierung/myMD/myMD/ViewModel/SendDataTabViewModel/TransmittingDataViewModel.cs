using Xamarin.Forms.Internals;
using Plugin.BluetoothLE;
using Plugin.BluetoothLE.Server;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;

namespace myMD.ViewModel.SendDataTabViewModel
{
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        IGattServer server;
        IDisposable notifyBroadcast;
        
        public TransmittingDataViewModel()
        {
                    
        }

        void BuildServer()
        {
            if (this.server != null)
                return;
            
            try
            {
                this.server = this.BleAdapter.CreateGattServer();
            } catch (Exception ex){
                Debug.WriteLine("Error building gatt server - " + ex);
            }
        }

        public void StartServer(){
            Debug.WriteLine("Hallo");
            if (this.BleAdapter.Status != AdapterStatus.PoweredOn)
            {
                Debug.WriteLine("Nope");
                return;
            }

            var server = CrossBleAdapter.Current.CreateGattServer();
            server.Start(new AdvertisementData
            {
                LocalName = "TestServer"
            });
            Debug.WriteLine(server.IsRunning);
        }

    }
}
