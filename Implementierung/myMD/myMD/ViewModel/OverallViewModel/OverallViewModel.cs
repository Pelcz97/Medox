using System.ComponentModel;
using System.Runtime.CompilerServices;
using myMD.Model.TransmissionModel;
using myMD.ModelInterface.ModelFacadeInterface;
using nexus.protocols.ble;
using Xamarin.Forms.Internals;


namespace myMD.ViewModel.OverallViewModel
{
    [Preserve(AllMembers = true)]
    /// <summary>
    /// ViewModel Elternklasse erbt an alle anderen ViewModel, um Redundanz der ModelFacade zu vermeiden
    /// </summary>
    public class OverallViewModel : INotifyPropertyChanged
    {
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

        protected IBluetoothLowEnergyAdapter BluetoothAdapter => Xamarin.Forms.DependencyService.Get<IBluetoothHelper>().Adapter;

    }
}
