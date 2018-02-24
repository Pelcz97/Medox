using myMDesktop.Model.TransmissionModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(myMDesktop.UWP.Model.TransmissionModel.Server))]
namespace myMDesktop.UWP.Model.TransmissionModel
{
    class Server : IServer
    {
        public static Guid myMDguid = new Guid("00000000-1000-1000-1000-00805F9B0000");
        
        public static Guid myMDserviceGuid1 = new Guid("10000000-1000-1000-1000-100000000000");
        public static Guid myMDserviceGuid2 = new Guid("20000000-2000-2000-2000-200000000000");
        public static Guid myMDcharGuid1 = new Guid("30000000-3000-3000-3000-300000000000");
        public static Guid myMDcharGuid2 = new Guid("40000000-4000-4000-4000-400000000000");

        public GattServiceProvider serviceProvider;
        private GattLocalCharacteristic myMDChar1;
        private bool peripheralSupported;

        
        //private GattLocalCharacteristic myMDChar2;
        //private GattLocalCharacteristic myMDChar3;

        public static readonly GattLocalCharacteristicParameters gattOperatorParameters = new GattLocalCharacteristicParameters
        {
            CharacteristicProperties = GattCharacteristicProperties.Write |
                                       GattCharacteristicProperties.WriteWithoutResponse,
            WriteProtectionLevel = GattProtectionLevel.Plain,
            UserDescription = "Operator Characteristic"
        };

        public Server()
        {
            checkPeripheralSupport();
        }

        public async void checkPeripheralSupport()
        {
           peripheralSupported = await CheckPeripheralRoleSupportAsync();
        }

        public async void StartServer()
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
                StartServer();
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
                myMDChar1 = result.Characteristic;
            }
            else
            {
                Debug.WriteLine(result.Error);
                return false;
            }

            
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
            serviceProvider.StartAdvertising(advParameters);
            return true;
        }

    }

    
}
