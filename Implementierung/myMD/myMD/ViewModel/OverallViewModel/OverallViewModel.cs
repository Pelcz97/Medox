using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using myMD.Model.TransmissionModel;
using myMD.ModelInterface.ModelFacadeInterface;
using Xamarin.Forms.Internals;
using Plugin.BluetoothLE;
using System;

namespace myMD.ViewModel.OverallViewModel
{
    [Preserve(AllMembers = true)]
    /// <summary>
    /// ViewModel Elternklasse erbt an alle anderen ViewModel, um Redundanz der ModelFacade zu vermeiden
    /// </summary>
    public class OverallViewModel : INotifyPropertyChanged
    {
        
        public static Guid myMDserviceGuid1 = new Guid("10000000-1000-1000-1000-100000000000");
        public static Guid myMDcharGuid1 = new Guid("30000000-3000-3000-3000-300000000000");
        public static Guid myMDFileCounterChar = new Guid("90000000-9000-9000-9000-900000000000");


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
