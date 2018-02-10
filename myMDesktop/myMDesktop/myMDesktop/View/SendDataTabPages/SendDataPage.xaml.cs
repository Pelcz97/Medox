﻿using System;
using myMDesktop.View.AbstractPages;
using myMDesktop.ViewModel.SendDataTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMDesktop.View.SendDataTabPages
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
        /// Methode, wenn der Button "Zu sendende Daten wählen" geklickt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void SendDataButton_Clicked(object sender, EventArgs e)
        {
            var page = new TransmittingDataPage();

            NavigationPage.SetBackButtonTitle(page, "Senden");
            Navigation.PushAsync(page);
        }
    }
}
