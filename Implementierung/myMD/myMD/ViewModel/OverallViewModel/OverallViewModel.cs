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
        
        protected static Guid myMD_FileTransfer = new Guid("88800000-8000-8000-8000-800000000000");

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
