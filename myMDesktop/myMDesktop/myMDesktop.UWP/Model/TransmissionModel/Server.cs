using myMDesktop.Model.TransmissionModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Storage.Streams;
using Xamarin.Forms;
using Plugin.FilePicker.Abstractions;
using System.Collections.ObjectModel;

[assembly: Xamarin.Forms.Dependency(typeof(myMDesktop.UWP.Model.TransmissionModel.Server))]
namespace myMDesktop.UWP.Model.TransmissionModel
{
    class Server : IServer
    {
        
        public static Guid myMDserviceGuid1 = new Guid("88800000-8000-8000-8000-800000000000");

        public static Guid RequestFile = new Guid("10000000-1000-1000-1000-100000000000");
        public static Guid RequestNumberOfSlices = new Guid("20000000-2000-2000-2000-200000000000");
        public static Guid RequestNumberOfFiles = new Guid("30000000-3000-3000-3000-300000000000");

        public GattServiceProvider serviceProvider;
        
        private GattLocalCharacteristic RequestFileCharacteristic;
        private GattLocalCharacteristic RequestSlicesNumberCharachteristic;
        private GattLocalCharacteristic RequestFilesNumberCharacteristic;

        private bool peripheralSupported;
        private int numberOfReadCycles { get; set; }
        public ObservableCollection<FileData> DoctorsLetters { get; set; }
        public Collection<IEnumerable<byte[]>> SplittedFiles { get; set; }


        public static readonly GattLocalCharacteristicParameters RequestFileChar = new GattLocalCharacteristicParameters
        {
            CharacteristicProperties = GattCharacteristicProperties.Write |
                                        GattCharacteristicProperties.Read |
                                       GattCharacteristicProperties.Notify,
            ReadProtectionLevel = GattProtectionLevel.Plain,
            UserDescription = "Request and Respond"
        };

        public Server()
        {
            SplittedFiles = new Collection<IEnumerable<byte[]>>();
            DoctorsLetters = new ObservableCollection<FileData>();
            checkPeripheralSupport();
        }

        public async void checkPeripheralSupport()
        {
           peripheralSupported = await CheckPeripheralRoleSupportAsync();
        }

        public async Task StartServer()
        {
            // Server not initialized yet - initialize it and start publishing
            if (serviceProvider == null)
            {
                var serviceStarted = await ServiceProviderInitAsync();
                if (serviceStarted)
                { 
                    Debug.WriteLine("Server started successfully.");
                }
                else
                {
                    Debug.WriteLine("Server could not start.");
                }
            }
            else
            { 
                serviceProvider.StopAdvertising();
                serviceProvider = null;
                await StartServer();
            }
        }

        private async Task<bool> CheckPeripheralRoleSupportAsync()
        {
            var localAdapter = await BluetoothAdapter.GetDefaultAsync();

            if (localAdapter != null)
            {
                return localAdapter.IsPeripheralRoleSupported;
            }
            else
            { 
                return false;
            }
        }

        private async Task<bool> ServiceProviderInitAsync()
        {
            // Initialize and starting a custom GATT Service using GattServiceProvider.
            GattServiceProviderResult serviceResult = await GattServiceProvider.CreateAsync(myMDserviceGuid1);
            
            if (serviceResult.Error == BluetoothError.Success)
            {
                serviceProvider = serviceResult.ServiceProvider;
            }
            else
            {
                Debug.WriteLine(serviceResult.Error);
                return false;
            }

            GattLocalCharacteristicResult requestFile = await serviceProvider.Service.CreateCharacteristicAsync(RequestFile, RequestFileChar);
            if (requestFile.Error == BluetoothError.Success)
            {
                RequestFileCharacteristic = requestFile.Characteristic;
            }
            else
            {
                Debug.WriteLine(requestFile.Error);
                return false;
            }
            RequestFileCharacteristic.WriteRequested += RequestFile_WriteRequestedAsync;
            RequestFileCharacteristic.ReadRequested += RequestFile_ReadRequestedAsync;

            //Indicate if sever advertises as connectable and discoverable.
            GattServiceProviderAdvertisingParameters advParameters = new GattServiceProviderAdvertisingParameters
            {
                IsConnectable = peripheralSupported,
                IsDiscoverable = true
            };

            Debug.WriteLine("Advertisment Status: " + serviceProvider.AdvertisementStatus);
            serviceProvider.StartAdvertising(advParameters);
            return true;
        }
        
        //Request and Respond Characteristic Methods
        int RequestFileIndex;
        int RequestSplitIndex;

        private async void RequestFile_WriteRequestedAsync(GattLocalCharacteristic sender, GattWriteRequestedEventArgs args)
        {
            using (args.GetDeferral())
            {
                
                GattWriteRequest request = await args.GetRequestAsync();
                if (request == null)
                {
                    Debug.WriteLine("Access to device not allowed");
                    return;
                }

                var dataReader = DataReader.FromBuffer(request.Value);
                var output = dataReader.ReadString(request.Value.Length);

                RequestFileIndex = Int32.Parse(output.Split(",")[0]);
                RequestSplitIndex = Int32.Parse(output.Split(",")[1]);

                Debug.WriteLine("output: " + output);
            }
        }

        private async void RequestFile_ReadRequestedAsync(GattLocalCharacteristic sender, GattReadRequestedEventArgs args)
        {
            using (args.GetDeferral())
            {
                
                GattReadRequest request = await args.GetRequestAsync();
                if (request == null)
                {
                    Debug.WriteLine("Access to device not allowed");
                    return;
                }

                var writer = new DataWriter();
                writer.ByteOrder = ByteOrder.LittleEndian;
                var array = SplittedFiles.ElementAt(RequestFileIndex).ElementAt(RequestSplitIndex);
                Debug.WriteLine("Array inhalt: " + BitConverter.ToString(array));
                writer.WriteBytes(array);
                request.RespondWithValue(writer.DetachBuffer());
            }
        }


    }

    
}
