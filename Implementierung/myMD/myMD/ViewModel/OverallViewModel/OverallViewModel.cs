using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using myMD.Model.TransmissionModel;
using myMD.ModelInterface.ModelFacadeInterface;
using Xamarin.Forms.Internals;
using Plugin.BluetoothLE;

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

        public static IAdapter BluetoothAdapter { get; set; }

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
            BluetoothAdapter = CrossBleAdapter.Current;

            /*BluetoothAdapter.WhenStatusChanged().Subscribe(state =>
            {
                Debug.WriteLine("New State: {0}", state);
                if (state == AdapterStatus.PoweredOn)
                {
                    StartScan();
                }
            });*/
        }



    }
}
