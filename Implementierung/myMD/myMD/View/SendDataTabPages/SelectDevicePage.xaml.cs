using System;
using myMD.View.AbstractPages;
using Xamarin.Forms;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    [Preserve(AllMembers = true)]
    public partial class SelectDevicePage : CustomContentPage
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
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void CancelSelectDevice_Clicked(object sender, EventArgs e)
        {
            vm.StopScan();
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Methode, wenn ein Gerät aus der Liste ausgewählt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void DeviceItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            vm.ConnectToDevice(e.SelectedItem);
        }
    }
}
