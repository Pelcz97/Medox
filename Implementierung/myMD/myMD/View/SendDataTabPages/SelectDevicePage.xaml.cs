using System;
using myMD.View.AbstractPages;
using Xamarin.Forms;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms.Internals;
using System.Diagnostics;

namespace myMD.View.SendDataTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur SelectDevicePage
    /// </summary>
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
        /// Methode, wenn ein Gerät aus der Liste ausgewählt wird.
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        void DeviceItemSelected(object sender, SelectedItemChangedEventArgs e)
        {   
            vm.ConnectToDevice(e.SelectedItem);
        }


        async void CancelSelectDevice_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        async void ConfirmSelectedDevice_Clicked(object sender, System.EventArgs e)
        {
            vm.SetConnectedServer();
            await Navigation.PopModalAsync();
        }
    }
}
