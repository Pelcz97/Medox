using System;
using Xamarin.Forms;
using myMDesktop.ViewModel.SendDataTabViewModel;
using Xamarin.Forms.Internals;

namespace myMDesktop.View.SendDataTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur SelectDevicePage
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class SelectDevicePage : ContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        SelectDeviceViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.SendDataTabPages.SelectDevicePage"/> class.
        /// </summary>
        public SelectDevicePage()
        {
            InitializeComponent();
            vm = new SelectDeviceViewModel();
            this.BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn der Abbrechen-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        async void CancelSelectDevice_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Methode, wenn ein Gerät aus der Liste ausgewählt wird.
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        void DeviceItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }

        void ConfirmDevice_Clicked(object sender, System.EventArgs e)
        {
           
            Navigation.PopModalAsync();
        }
    }
}