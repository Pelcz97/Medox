using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    /// <summary>
    /// Code-Behind Klasse zurTransmittingDataPage.
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class TransmittingDataPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        TransmittingDataViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.SendDataTabPages.TransmittingDataPage"/> class.
        /// </summary>
        public TransmittingDataPage()
        {
            InitializeComponent();
            LoadingIndicator.IsVisible = false;
            vm = new TransmittingDataViewModel();
            BindingContext = vm;
        }

        async void SelectDevice_Clicked(object sender, System.EventArgs e)
        {
            var view = new NavigationPage(new SelectDevicePage());
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;

            await Navigation.PushModalAsync(view);
        }

        async void LoadLetters_Clicked(object sender, System.EventArgs e)
        {
            LoadingIndicator.IsVisible = true;
            await vm.ReceiveData();
            LoadingIndicator.IsVisible = false;
            await DisplayAlert("Übertragung abgeschlossen", "Die neuen Arztbriefe befinden sich nun in der Übersicht.", "Okay");
        }
    }
}
