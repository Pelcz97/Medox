using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Plugin.BluetoothLE;
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

        public static IAdapter BleAdapter { get; set; }

        /// <summary>
        /// Konstruktor für ein OverallViewModel
        /// </summary>
        public OverallViewModel()
        {

            //CrossBleAdapter.Current.WhenStatusChanged().Subscribe(status => { Debug.WriteLine("CrossBLEAdapterstatus : " + status); });
        }



    }
}
