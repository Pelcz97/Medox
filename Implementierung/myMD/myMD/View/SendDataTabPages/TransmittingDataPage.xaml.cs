﻿using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    /// <summary>
    /// Transmitting data page.
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
            BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn der Button zur Wahl der zu sendenden Arztbriefe geklickt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public async void SelectLettersButton_Clicked(object sender, EventArgs e)
        {
            var view = new NavigationPage(new SelectDoctorsLettersPage());
            view.BarTextColor = Color.White;
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            await Navigation.PushModalAsync(view);
        }

        /// <summary>
        /// Methode, wenn der Button zur Wahl eines Empfängergerätes geklickt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public async void SelectDeviceButton_Clicked(object sender, EventArgs e)
        {
            var view = new NavigationPage(new SelectDevicePage()); 
            view.BarTextColor = Color.White;
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            await Navigation.PushModalAsync(view);
        }
    }
}
