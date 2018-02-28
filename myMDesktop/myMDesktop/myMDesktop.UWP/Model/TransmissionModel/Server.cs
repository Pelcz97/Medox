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
        public static Guid myMDResponseWrite = new Guid("60000000-6000-6000-6000-600000000000");
        public static Guid myMDreadCycleCount = new Guid("70000000-7000-7000-7000-700000000000");


        public GattServiceProvider serviceProvider;
        private GattLocalCharacteristic myMDCharacteristic;
        private GattLocalCharacteristic myMDFileCountChar;
        private GattLocalCharacteristic myMDreadCycleCountChar;
        private GattLocalCharacteristic myMDResponseWriteChar;
        private bool peripheralSupported;
        private int numberOfReadCycles { get; set; }
        public ObservableCollection<FileData> DoctorsLetters { get; set; }
        public Collection<IEnumerable<byte[]>> SplittedFiles { get; set; }


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

        public static readonly GattLocalCharacteristicParameters ResponseWrite = new GattLocalCharacteristicParameters
        {
            CharacteristicProperties = GattCharacteristicProperties.Write,
            WriteProtectionLevel = GattProtectionLevel.Plain,
            UserDescription = "myMD ResponseWrite"
        };

        public static readonly GattLocalCharacteristicParameters ReadCycleChar = new GattLocalCharacteristicParameters
        {
            CharacteristicProperties = GattCharacteristicProperties.Read,
            ReadProtectionLevel = GattProtectionLevel.Plain,
            UserDescription = "myMD ResponseWrite"
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

            GattLocalCharacteristicResult readCycleCount = await serviceProvider.Service.CreateCharacteristicAsync(myMDreadCycleCount, ReadCycleChar);
            if (result.Error == BluetoothError.Success)
            {
                myMDreadCycleCountChar = readCycleCount.Characteristic;
            }
            else
            {
                Debug.WriteLine(fileCount.Error);
                return false;
            }
            myMDreadCycleCountChar.ReadRequested += CycleCount_ReadRequestedAsync;

            GattLocalCharacteristicResult responseWrite = await serviceProvider.Service.CreateCharacteristicAsync(myMDResponseWrite, ResponseWrite);
            if (result.Error == BluetoothError.Success)
            {
                myMDResponseWriteChar = responseWrite.Characteristic;
            }
            else
            {
                Debug.WriteLine(responseWrite.Error);
                return false;
            }
            myMDResponseWriteChar.WriteRequested += ResponseWrite_WriteRequestedAsync;


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
                    writer.WriteInt32(SplittedFiles.Count);

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

        private async void CycleCount_ReadRequestedAsync(GattLocalCharacteristic sender, GattReadRequestedEventArgs args)
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
                writer.WriteInt32(SplittedFiles.ElementAt(CurrentFileIndex).Count());

                request.RespondWithValue(writer.DetachBuffer());
            }
        }

        int CurrentFileIndex = 0;
        int CurrentSplitIndex = 0;
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
                var array = SplittedFiles.ElementAt(CurrentFileIndex).ElementAt(CurrentSplitIndex);
                

                Debug.WriteLine("Array: " + array);
                
                writer.WriteBytes(array);

                if(CurrentSplitIndex <= SplittedFiles.ElementAt(CurrentFileIndex).Count())
                {
                    CurrentSplitIndex++;
                } else
                {
                    if(CurrentFileIndex <= SplittedFiles.Count())
                    {
                        CurrentFileIndex++;
                    }   
                }
                
                request.RespondWithValue(writer.DetachBuffer());
               
            }
        }

        private async void ResponseWrite_WriteRequestedAsync(GattLocalCharacteristic sender, GattWriteRequestedEventArgs args)
        {
            // BT_Code: Processing a write request.
            using (args.GetDeferral())
            {
                // Get the request information.  This requires device access before an app can access the device's request.
                GattWriteRequest request = await args.GetRequestAsync();
                if (request == null)
                {
                    return;
                }

                var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(request.Value);
                var output = dataReader.ReadString(request.Value.Length);
                Debug.WriteLine("Wert: " + output);
            }
        }



    }

    
}
