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
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel.OverallViewModel
    {
        /// <summary>
        /// Der Bluetooth GATT-Server, der die zu sendenden Arztbriefe einem Klient bereitstellen soll.
        /// </summary>
        IGattServer server;

        /// <summary>
        /// The notify broadcast.
        /// </summary>
        IDisposable notifyBroadcast;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel()
        {
            AdapterStatus status = CrossBleAdapter.Current.Status;
            BuildServer();
        }

        /// <summary>
        /// Methode zur Verwaltung des Servers:
        /// Zu Testzwecken wird zunächst der Adapter Status ausgegeben.
        /// Anschliessend wird versucht, einen Server zu erstellen, zu Starten und mit einem Namen 
        /// anderen Geräten in der Nähe sichtbar zu machen.
        /// </summary>
        public async void ServerSettings(){
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
                    await server.Start(new AdvertisementData
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

        /// <summary>
        /// Explizite Zusammensetzung des Servers.
        /// Hier werden die einzelnen Charakteristiken erstellt, die notwendig sind, 
        /// um Daten schreiben und lesen zu können sowie eine Benachrichtigung an Klienten versenden zu können.
        /// Die hier angegeben Charakteristiken dienen Testzwecken, 
        /// da ein grundlegendes Problem mit dem Server besteht.
        /// </summary>
        public void BuildServer()
        {
            server = CrossBleAdapter.Current.CreateGattServer();
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

            notifyBroadcast = null;
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
        }
    }
}
