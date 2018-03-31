using System;
using myMD.View.AbstractPages;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace myMD.View.SendDataTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur SendDataPage.
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class SendDataPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        SendDataViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.SendDataTabPages.SendDataPage"/> class.
        /// </summary>
        public SendDataPage()
        {
            InitializeComponent();
            vm = new SendDataViewModel();
            BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn der Button "Daten empfangen" geklickt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void ReceiveDataButton_Clicked(object sender, EventArgs e)
        {
            var page = new TransmittingDataPage();

            Xamarin.Forms.NavigationPage.SetBackButtonTitle(page, "Senden");
            await Navigation.PushAsync(page);
        }
    }
}
