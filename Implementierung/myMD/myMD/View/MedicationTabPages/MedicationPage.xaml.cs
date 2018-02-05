using System;
using System.Collections.Generic;
using System.Diagnostics;
using myMD.View.AbstractPages;
using myMD.ViewModel.MedicationTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.MedicationTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur MedicationPage
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class MedicationPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        MedicationViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.MedicationTabPages.MedicationPage"/> class.
        /// </summary>
        public MedicationPage()
        {
            InitializeComponent();
            vm = new MedicationViewModel();
            BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn der Hinzufügen-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        public async void AddMed_Clicked(object sender, EventArgs e)
        {
            var view = new NavigationPage(new DetailedMedicationPage());
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;

            await Navigation.PushModalAsync(view);
        }

        /// <summary>
        /// Methode, wenn ein Element der Medikationenliste geklickt wird.
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        void MedicationItem_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var view = new NavigationPage(new DetailedMedicationPage(e.SelectedItem));
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;
            Navigation.PushModalAsync(view);
        }
    }
}