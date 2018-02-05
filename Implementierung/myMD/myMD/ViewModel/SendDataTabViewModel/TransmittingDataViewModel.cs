using Xamarin.Forms.Internals;
using Plugin.BluetoothLE;
using Plugin.BluetoothLE.Server;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace myMD.ViewModel.SendDataTabViewModel
{
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        IGattServer server;
        IDisposable notifyBroadcast;

        string output;
        public string Output
        {
            get => output;
            private set => output = value;
        }

        string serverText = "Start Server";
        public string ServerText
        {
            get => this.serverText;
            set => serverText = value;
        }

        string chValue;
        public string CharacteristicValue
        {
            get => this.chValue;
            set => chValue = value;
        }

        public TransmittingDataViewModel()
        {

            AdapterStatus status = CrossBleAdapter.Current.Status;
            BuildServer();

        }

        public void ServerSettings(){
            if (this.BleAdapter.Status != AdapterStatus.PoweredOn)
            {
                Debug.WriteLine(this.BleAdapter.Status);
                return;
            }

            try
            {
                this.BuildServer();
                if (this.server.IsRunning)
                {
                    this.server.Stop();
                }
                else
                {
                     server.Start(new AdvertisementData
                    {
                        LocalName = "TestServer"
                    });

                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        public async void BuildServer()
        {
            var server = CrossBleAdapter.Current.CreateGattServer();
            var service = server.AddService(Guid.NewGuid(), true);

            var characteristic = service.AddCharacteristic(
                Guid.NewGuid(),
                CharacteristicProperties.Read | CharacteristicProperties.Write,
                GattPermissions.Read | GattPermissions.Write
            );

            var notifyCharacteristic = service.AddCharacteristic
            (
                Guid.NewGuid(),
                CharacteristicProperties.Indicate | CharacteristicProperties.Notify,
                GattPermissions.Read | GattPermissions.Write
            );

            IDisposable notifyBroadcast = null;
            notifyCharacteristic.WhenDeviceSubscriptionChanged().Subscribe(e =>
            {
                var @event = e.IsSubscribed ? "Subscribed" : "Unsubcribed";

                if (notifyBroadcast == null)
                {
                    this.notifyBroadcast = Observable
                        .Interval(TimeSpan.FromSeconds(1))
                        .Where(x => notifyCharacteristic.SubscribedDevices.Count > 0)
                        .Subscribe(_ =>
                        {
                            Debug.WriteLine("Sending Broadcast");
                            var dt = DateTime.Now.ToString("g");
                            var bytes = Encoding.UTF8.GetBytes(dt);
                            notifyCharacteristic.Broadcast(bytes);
                        });
                }
            });

            characteristic.WhenReadReceived().Subscribe(x =>
            {
                var write = "HELLO";

                // you must set a reply value
                x.Value = Encoding.UTF8.GetBytes(write);

                x.Status = GattStatus.Success; // you can optionally set a status, but it defaults to Success
            });
            characteristic.WhenWriteReceived().Subscribe(x =>
            {
                var write = Encoding.UTF8.GetString(x.Value, 0, x.Value.Length);
                // do something value
            });
            try
            {
                await server.Start(new AdvertisementData
                {
                    LocalName = "TestServer"
                });
            } catch (Exception e){
                Debug.WriteLine(e.ToString());
            }
        }

        void OnEvent(string msg)
        {
            Device.BeginInvokeOnMainThread(() =>
                this.Output += msg + Environment.NewLine + Environment.NewLine
            );
        }

        void StopServer(){
            
        }
    }
}
