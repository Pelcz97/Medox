using Xamarin.Forms.Internals;
using Plugin.BluetoothLE;
using Plugin.BluetoothLE.Server;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Text;
using System.Reactive.Linq;

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


        public TransmittingDataViewModel()
        {

            AdapterStatus status = CrossBleAdapter.Current.Status;

            CrossBleAdapter.Current.WhenStatusChanged().Subscribe(x =>
            {
                Debug.WriteLine("AdapterStatus: " + status);
            });

            if (status == AdapterStatus.PoweredOn)
            {
                StartServer();
                if (server.IsRunning)
                {
                    server.Stop();
                }
                else
                {
                    server.Start(new AdvertisementData
                    {
                        LocalName = "TestServer"
                    });
                }
            }
            else
            {
                Debug.WriteLine("Status is: " + status);
            }
        }

        public void StartServer()
        {
            Debug.WriteLine("Entered StartServerMethod");
            if (this.server != null){
                Debug.WriteLine("Fuck"); 
                return;
            }

            this.server = CrossBleAdapter.Current.CreateGattServer();
            var service = this.server.AddService(Guid.NewGuid(), true);

            /*var characteristic = service.AddCharacteristic(
                    Guid.NewGuid(),
                    CharacteristicProperties.Read | CharacteristicProperties.Write | CharacteristicProperties.WriteNoResponse,
                    GattPermissions.Read | GattPermissions.Write
                );

            var notifyCharacteristic = service.AddCharacteristic
                (
                    Guid.NewGuid(),
                    CharacteristicProperties.Notify,
                    GattPermissions.Read | GattPermissions.Write
                );

            notifyCharacteristic.WhenDeviceSubscriptionChanged().Subscribe(e =>
            {
                var @event = e.IsSubscribed ? "Subscribed" : "Unsubcribed";
                this.OnEvent($"Device {e.Device.Uuid} {@event}");
                this.OnEvent($"Charcteristic Subcribers: {notifyCharacteristic.SubscribedDevices.Count}");

                if (this.notifyBroadcast == null)
                {
                    this.OnEvent("Starting Subscriber Thread");
                    this.notifyBroadcast = Observable
                        .Interval(TimeSpan.FromSeconds(1))
                        .Where(x => notifyCharacteristic.SubscribedDevices.Count > 0)
                        .Subscribe(_ =>
                        {
                            try
                            {
                                var dt = DateTime.Now.ToString("g");
                                var bytes = Encoding.UTF8.GetBytes(dt);
                                notifyCharacteristic
                                    .BroadcastObserve(bytes)
                                    .Subscribe(x =>
                                    {
                                        var state = x.Success ? "Successfully" : "Failed";
                                        var data = Encoding.UTF8.GetString(x.Data, 0, x.Data.Length);
                                        this.OnEvent($"{state} Broadcast {data} to device {x.Device.Uuid} from characteristic {x.Characteristic}");
                                    });
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Error during broadcast: " + ex);
                            }
                        });
                }
            });*/
        }

        void OnEvent(string msg)
        {
            Device.BeginInvokeOnMainThread(() =>
                this.Output += msg + Environment.NewLine + Environment.NewLine
            );
        }
    }
}
