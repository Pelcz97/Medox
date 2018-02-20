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
        public static Guid myMDguid = new Guid("c531ba9d-2ff3-48e9-8f15-808beb02bdf9");
        public static Guid myMDserviceGuid1 = new Guid("10000000-1000-1000-1000-100000000000");
        public static Guid myMDserviceGuid2 = new Guid("20000000-2000-2000-2000-200000000000");
        public static Guid myMDcharGuid1 = new Guid("30000000-3000-3000-3000-300000000000");
        public static Guid myMDcharGuid2 = new Guid("40000000-4000-4000-4000-400000000000");

        public string Output
        {
            get => this.output;
            private set => this.output = value;
        }

        private IDisposable notifyBroadcast;
        private IGattServer server { get; set; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:myMD.ViewModel.SendDataTabViewModel.TransmittingDataViewModel"/> class.
        /// </summary>
        public TransmittingDataViewModel() {

            FindAdapter();
            
            /*MessagingCenter.Subscribe<SelectDoctorsLettersViewModel, ObservableCollection<DoctorsLetterViewModel>>(this, "SelectedLetters", (sender, arg) => {
                LettersToSend = arg;
            });*/
            /*MessagingCenter.Subscribe<SelectDeviceViewModel, IBlePeripheral>(this, "ConnectedDevice", (sender, arg) => {
                 TargetDevice = arg;
                 Debug.WriteLine("SelectedDevice : " + TargetDevice.Advertisement.DeviceName);
             });*/
        }

        public async void FindAdapter()
        {
            BleAdapter = await CrossBleAdapter.AdapterScanner.FindAdapters();
            StartServer();
        }

        public async void StartServer()
        {
            if (BleAdapter.Status != AdapterStatus.PoweredOn)
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
                        ManufacturerData = null,
                        ServiceUuids = new List<Guid> { myMDserviceGuid1 }
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
            server = BleAdapter.CreateGattServer();
            var service = server.AddService(myMDserviceGuid1, true);

            var characteristic = service.AddCharacteristic(
                myMDcharGuid1,
                CharacteristicProperties.Read | CharacteristicProperties.Write,
                GattPermissions.Read
            );


            IDisposable notifyBroadcast = null;
            characteristic.WhenDeviceSubscriptionChanged().Subscribe(e =>
            {
                var @event = e.IsSubscribed ? "Subscribed" : "Unsubcribed";

                if (notifyBroadcast == null)
                {
                    this.notifyBroadcast = Observable
                        .Interval(TimeSpan.FromSeconds(1))
                        .Where(x => characteristic.SubscribedDevices.Count > 0)
                        .Subscribe(_ =>
                        {
                            Debug.WriteLine("Sending Broadcast");
                            var dt = DateTime.Now.ToString("g");
                            var bytes = Encoding.UTF8.GetBytes(dt);
                            characteristic.Broadcast(bytes);
                        });
                }
            });

            characteristic.WhenReadReceived().Subscribe(x =>
            {
                try {
                    var write = "HELLO";

                // you must set a reply value
                x.Value = Encoding.UTF8.GetBytes(write);

                x.Status = GattStatus.Success; // you can optionally set a status, but it defaults to Success 
                } catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            });

            characteristic.WhenWriteReceived().Subscribe(x =>
            {
                //var write = Encoding.UTF8.GetString(x.Value, 0, x.Value.Length);
                // do something value
                Debug.WriteLine("Neuer Wert empfangen.");
            });
        }
        

        void OnEvent(string msg)
        {
            Device.BeginInvokeOnMainThread(() =>
                this.Output += msg + Environment.NewLine + Environment.NewLine
            );
        }
    }
}
