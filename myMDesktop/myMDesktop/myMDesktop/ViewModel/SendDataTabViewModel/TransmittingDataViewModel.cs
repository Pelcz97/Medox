using Xamarin.Forms.Internals;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using myMD.ViewModel.OverallViewModel;
using Plugin.BluetoothLE;
using Plugin.BluetoothLE.Server;
using System.Text;
using System.Reactive.Linq;
using ReactiveUI;
using System.Collections.Generic;

namespace myMDesktop.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Transmitting data view model.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TransmittingDataViewModel : OverallViewModel
    {

        string output;
        public static Guid myMDguid = new Guid("00000000-1000-1000-1000-00805F9B0000");
        public string Output
        {
            get => this.output;
            private set => this.output = value;
        }

        private IDisposable notifyBroadcast;
        private IGattServer server { get; set; }

        private IAdapter BleAdapter { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel() { 

            
            CrossBleAdapter.Current.WhenStatusChanged().Subscribe(status => { Debug.WriteLine("CrossBLEAdapterstatus : " + status); });
            Debug.WriteLine("status : " + CrossBleAdapter.Current.Status);
            StartServer();

            /*MessagingCenter.Subscribe<SelectDoctorsLettersViewModel, ObservableCollection<DoctorsLetterViewModel>>(this, "SelectedLetters", (sender, arg) => {
                LettersToSend = arg;
            });*/
            /*MessagingCenter.Subscribe<SelectDeviceViewModel, IBlePeripheral>(this, "ConnectedDevice", (sender, arg) => {
                 TargetDevice = arg;
                 Debug.WriteLine("SelectedDevice : " + TargetDevice.Advertisement.DeviceName);
             });*/
        }

        public async void StartServer()
        {
            if (CrossBleAdapter.Current.Status != AdapterStatus.PoweredOn)
            {
                Debug.WriteLine("Couldnt start server");
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
                    await this.server.Start(new AdvertisementData
                    {
                        LocalName = "TestServer",
                        ServiceUuids = new List<Guid> { myMDguid }
                    });
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
            }
        }

        public async void BuildServer()
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
        }

        void BuildServerPluginLike()
        {
            if (this.server != null)
                return;

            try
            {
                this.server = CrossBleAdapter.Current.CreateGattServer();
                var service = this.server.AddService(Guid.NewGuid(), true);

                var characteristic = service.AddCharacteristic(
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

                //var descriptor = characteristic.AddDescriptor(Guid.NewGuid(), Encoding.UTF8.GetBytes("Test Descriptor"));

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
                });

                
                characteristic.WhenWriteReceived().Subscribe(x =>
                {
                    var write = Encoding.UTF8.GetString(x.Value, 0, x.Value.Length);
                    this.OnEvent($"Characteristic Write Received - {write}");
                });

                this.server
                    .WhenRunningChanged()
                    .Catch<bool, ArgumentException>(ex =>
                    {
                        Debug.WriteLine("Error Starting GATT Server - " + ex);
                        return Observable.Return(false);
                    })
                    .Subscribe(started => Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!started)
                        {
                            
                            this.OnEvent("GATT Server Stopped");
                        }
                        else
                        {
                            this.notifyBroadcast?.Dispose();
                            this.notifyBroadcast = null;

                            
                            this.OnEvent("GATT Server Started");
                            foreach (var s in this.server.Services)
                            {
                                this.OnEvent($"Service {s.Uuid} Created");
                                foreach (var ch in s.Characteristics)
                                {
                                    this.OnEvent($"Characteristic {ch.Uuid} Online - Properties {ch.Properties}");
                                }
                            }
                        }
                    }));

                this.server
                    .WhenAnyCharacteristicSubscriptionChanged()
                    .Subscribe(x =>
                        this.OnEvent($"[WhenAnyCharacteristicSubscriptionChanged] UUID: {x.Characteristic.Uuid} - Device: {x.Device.Uuid} - Subscription: {x.IsSubscribing}")
                    );

                //descriptor.WhenReadReceived().Subscribe(x =>
                //    this.OnEvent("Descriptor Read Received")
                //);
                //descriptor.WhenWriteReceived().Subscribe(x =>
                //{
                //    var write = Encoding.UTF8.GetString(x.Value, 0, x.Value.Length);
                //    this.OnEvent($"Descriptor Write Received - {write}");
                //});
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error building gatt server - " + ex);
            }
        }

        void OnEvent(string msg)
        {
            Device.BeginInvokeOnMainThread(() =>
                this.Output += msg + Environment.NewLine + Environment.NewLine
            );
        }
    }
}
