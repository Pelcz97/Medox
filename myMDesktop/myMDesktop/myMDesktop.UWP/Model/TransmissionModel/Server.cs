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
        public static Guid myMDcharGuid1 = new Guid("50000000-5000-5000-5000-500000000000");
        public static Guid myMDfileCount = new Guid("40000000-4000-4000-4000-400000000000");


        public GattServiceProvider serviceProvider;
        private GattLocalCharacteristic myMDCharacteristic;
        private GattLocalCharacteristic myMDFileCountChar;
        private bool peripheralSupported;

        public ObservableCollection<FileData> DoctorsLetters { get; set; }

        public static readonly GattLocalCharacteristicParameters gattOperatorParameters = new GattLocalCharacteristicParameters
        {
            CharacteristicProperties = GattCharacteristicProperties.Read |
                                       GattCharacteristicProperties.Notify,
            ReadProtectionLevel = GattProtectionLevel.Plain,
            UserDescription = "myMD FileReader"
        };

        public static readonly GattLocalCharacteristicParameters ReadChar = new GattLocalCharacteristicParameters
        {
            CharacteristicProperties = GattCharacteristicProperties.Read |
                                       GattCharacteristicProperties.Notify,
            ReadProtectionLevel = GattProtectionLevel.Plain,
            UserDescription = "myMD FileCounter"
        };

        public Server()
        {
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
                // BT_Code: Stops advertising support for custom GATT Service 
                serviceProvider.StopAdvertising();
                serviceProvider = null;
                await StartServer();
            }
        }

        private async Task<bool> CheckPeripheralRoleSupportAsync()
        {
            // BT_Code: New for Creator's Update - Bluetooth adapter has properties of the local BT radio.
            var localAdapter = await BluetoothAdapter.GetDefaultAsync();

            if (localAdapter != null)
            {
                return localAdapter.IsPeripheralRoleSupported;
            }
            else
            {
                // Bluetooth is not turned on 
                return false;
            }
        }

        private async Task<bool> ServiceProviderInitAsync()
        {
            // BT_Code: Initialize and starting a custom GATT Service using GattServiceProvider.
            GattServiceProviderResult serviceResult = await GattServiceProvider.CreateAsync(myMDserviceGuid1);
            //Debug.WriteLine("Service: " + serviceResult);
            if (serviceResult.Error == BluetoothError.Success)
            {
                serviceProvider = serviceResult.ServiceProvider;
            }
            else
            {
                Debug.WriteLine(serviceResult.Error);
                return false;
            }

            GattLocalCharacteristicResult result = await serviceProvider.Service.CreateCharacteristicAsync(myMDcharGuid1, gattOperatorParameters);
            if (result.Error == BluetoothError.Success)
            {
                myMDCharacteristic = result.Characteristic;
            }
            else
            {
                Debug.WriteLine(result.Error);
                return false;
            }
            myMDCharacteristic.ReadRequested += FileReader_ReadRequestedAsync;

            GattLocalCharacteristicResult fileCount = await serviceProvider.Service.CreateCharacteristicAsync(myMDfileCount, ReadChar);
            if (result.Error == BluetoothError.Success)
            {
                myMDFileCountChar = fileCount.Characteristic;
            }
            else
            {
                Debug.WriteLine(fileCount.Error);
                return false;
            }
            myMDFileCountChar.ReadRequested += FileCount_ReadRequestedAsync;


            // BT_Code: Indicate if your sever advertises as connectable and discoverable.
            GattServiceProviderAdvertisingParameters advParameters = new GattServiceProviderAdvertisingParameters
            {
                // IsConnectable determines whether a call to publish will attempt to start advertising and 
                // put the service UUID in the ADV packet (best effort)
                IsConnectable = peripheralSupported,

                // IsDiscoverable determines whether a remote device can query the local device for support 
                // of this service
                IsDiscoverable = true
            };
            //serviceProvider.AdvertisementStatusChanged += ServiceProvider_AdvertisementStatusChanged;
            Debug.WriteLine("Advertisment Status: " + serviceProvider.AdvertisementStatus);
            serviceProvider.StartAdvertising(advParameters);
            return true;
        }

        private async void FileCount_ReadRequestedAsync(GattLocalCharacteristic sender, GattReadRequestedEventArgs args)
        {
            using (args.GetDeferral())
            {
               // await CoreApplication.MainView.CoreWindow.Dispatcher.RunTaskAsync(async () =>
                //{
                    // Get the request information.  This requires device access before an app can access the device's request. 
                    GattReadRequest request = await args.GetRequestAsync();
                    if (request == null)
                    {
                        // No access allowed to the device.  Application should indicate this to the user.
                        Debug.WriteLine("Access to device not allowed");
                        return;
                    }

                    var writer = new DataWriter();
                    writer.ByteOrder = ByteOrder.LittleEndian;
                    writer.WriteInt32(DoctorsLetters.Count);

                    // Can get details about the request such as the size and offset, as well as monitor the state to see if it has been completed/cancelled externally.
                    // request.Offset
                    // request.Length
                    // request.State
                    // request.StateChanged += <Handler>

                    // Gatt code to handle the response
                    request.RespondWithValue(writer.DetachBuffer());
                //});
            }
        }

        private async void FileReader_ReadRequestedAsync(GattLocalCharacteristic sender, GattReadRequestedEventArgs args)
        {
            using (args.GetDeferral())
            {
                // await CoreApplication.MainView.CoreWindow.Dispatcher.RunTaskAsync(async () =>
                //{
                // Get the request information.  This requires device access before an app can access the device's request. 
                GattReadRequest request = await args.GetRequestAsync();
                if (request == null)
                {
                    // No access allowed to the device.  Application should indicate this to the user.
                    Debug.WriteLine("Access to device not allowed");
                    return;
                }

                var writer = new DataWriter();
                writer.ByteOrder = ByteOrder.LittleEndian;
                var array = DoctorsLetters.FirstOrDefault().DataArray;

                var batchSize = 500; //534?
                var batched = array
                    .Select((Value, Index) => new { Value, Index })
                    .GroupBy(p => p.Index / batchSize)
                    .Select(g => g.Select(p => p.Value).ToList());

                Debug.WriteLine(batched);
                Debug.WriteLine(array);
                writer.WriteBytes(array);

                // Can get details about the request such as the size and offset, as well as monitor the state to see if it has been completed/cancelled externally.
                // request.Offset
                // request.Length
                // request.State
                // request.StateChanged += <Handler>

                // Gatt code to handle the response
                request.RespondWithValue(writer.DetachBuffer());
                //});
            }
        }



    }

    
}
