using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur SelectDoctorsLettersPage
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class SelectDoctorsLettersPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        SelectDoctorsLettersViewModel vm;

        /// <summary>
        /// Kosntruktor für eine SelectDoctorsLetterPage
        /// </summary>
        public SelectDoctorsLettersPage()
        {
            InitializeComponent();
            vm = new SelectDoctorsLettersViewModel();
            BindingContext = vm;
        }

        /// <summary>
        /// Methode um eine SelectDoctosLetterPage zu schließen
        /// </summary>
        /// <param name="sender">Der Sender der diese Methode auslöst</param>
        /// <param name="e">Das Event, dass des Senders</param>
        async void CancelSelectDoctorsLetters_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        async void ConfirmSelectDoctorsLetters_Clicked(object sender, System.EventArgs e)
        {
            vm.SelectionConfirmed();
            await Navigation.PopModalAsync();
        }
    }
}