using myMDesktop.Model.TransmissionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

[assembly: DefaultDependency(typeof(myMDesktop.Model.TransmissionModel.UWP.Server))]
namespace myMDesktop.UWP.Model.TransmissionModel.UWP
{
    class Server : IServer
    {
        GattServiceProvider serviceProvider;
        private bool peripheralSupported;

        private GattLocalCharacteristic myMDChar1;
        private GattLocalCharacteristic myMDChar2;
        private GattLocalCharacteristic myMDChar3;
        

        Server()
        {
            peripheralSupported = await CheckPeripheralRoleSupportAsync();
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
            GattServiceProviderResult serviceResult = await GattServiceProvider.CreateAsync(Constants.CalcServiceUuid);
            if (serviceResult.Error == BluetoothError.Success)
            {
                serviceProvider = serviceResult.ServiceProvider;
            }
            else
            {
                rootPage.NotifyUser($"Could not create service provider: {serviceResult.Error}", NotifyType.ErrorMessage);
                return false;
            }

            GattLocalCharacteristicResult result = await serviceProvider.Service.CreateCharacteristicAsync(Constants.Op1CharacteristicUuid, Constants.gattOperandParameters);
            if (result.Error == BluetoothError.Success)
            {
                myMDChar1 = result.Characteristic;
            }
            else
            {
                rootPage.NotifyUser($"Could not create operand1 characteristic: {result.Error}", NotifyType.ErrorMessage);
                return false;
            }
            myMDChar1.WriteRequested += Op1Characteristic_WriteRequestedAsync;

            
            // Add presentation format - 32-bit unsigned integer, with exponent 0, the unit is unitless, with no company description
            GattPresentationFormat intFormat = GattPresentationFormat.FromParts(
                GattPresentationFormatTypes.UInt32,
                PresentationFormats.Exponent,
                Convert.ToUInt16(PresentationFormats.Units.Unitless),
                Convert.ToByte(PresentationFormats.NamespaceId.BluetoothSigAssignedNumber),
                PresentationFormats.Description);

            Constants.gattResultParameters.PresentationFormats.Add(intFormat);

            
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
            serviceProvider.AdvertisementStatusChanged += ServiceProvider_AdvertisementStatusChanged;
            serviceProvider.StartAdvertising(advParameters);
            return true;
        }

    }

    
}
