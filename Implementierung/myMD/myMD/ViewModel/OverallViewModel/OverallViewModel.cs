using System.ComponentModel;
using System.Runtime.CompilerServices;
using myMD.Model.TransmissionModel;
using myMD.ModelInterface.ModelFacadeInterface;
using Xamarin.Forms.Internals;
using System;
using nexus.protocols.ble;
using myMD.Model.DependencyService;

namespace myMD.ViewModel.OverallViewModel
{
    [Preserve(AllMembers = true)]
    /// <summary>
    /// ViewModel Elternklasse erbt an alle anderen ViewModel, um Redundanz der ModelFacade zu vermeiden
    /// </summary>
    public class OverallViewModel : INotifyPropertyChanged
    {
        
        public static Guid myMD_FileTransfer = new Guid("88800000-8000-8000-8000-800000000000");
        /*public static Guid DataCharacteristic = new Guid("50000000-5000-5000-5000-500000000000");
        public static Guid FileCounterCharacteristic = new Guid("40000000-4000-4000-4000-400000000000");
        public static Guid ReadResponse = new Guid("60000000-6000-6000-6000-600000000000");
        public static Guid myMDReadCycleCount = new Guid("70000000-7000-7000-7000-700000000000");
        public static Guid RequestAndRespond = new Guid("10000000-1000-1000-1000-100000000000");*/

        protected IBluetoothLowEnergyAdapter BluetoothAdapter { get => DependencyServiceWrapper.Get<IBluetoothHelper>().Adapter; }

        /// <summary>
        /// Attribut für die ModelFacade
        /// </summary>
        protected IModelFacade ModelFacade;

        /// <summary>
        /// Attribut für ein PropertyChangedEventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Methode um ein PropertyChangedEvent zu handeln
        /// </summary>
        /// <param name="propertyName">Der Name der Property, die geändert (changed) wurde.</param>
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Konstruktor für ein OverallViewModel
        /// </summary>
        public OverallViewModel()
        {
            ModelFacade = App.Model;
        }

    }
}
