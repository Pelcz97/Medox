using System;
using System.Collections.Generic;
using System.ComponentModel;
using myMD.Model.DataModel;
using myMD.View.AbstractPages;
using myMD.ViewModel.MedicationTabViewModel;
using Xamarin.Forms;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms.Internals;

namespace myMD.View.MedicationTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur DetailedMedicationPage
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class DetailedMedicationPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        DetailedMedicineViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.MedicationTabPages.DetailedMedicationPage"/> class.
        /// </summary>
        public DetailedMedicationPage()
        {
            InitializeComponent();

            vm = new DetailedMedicineViewModel();
            BindingContext = vm;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.MedicationTabPages.DetailedMedicationPage"/> class.
        /// </summary>
        /// <param name="item">DetailedMedication die geöffnet wird</param>
        public DetailedMedicationPage(object item)
        {
            InitializeComponent();

            vm = new DetailedMedicineViewModel(item);
            BindingContext = vm;
        }

        override
        protected bool OnBackButtonPressed()
        {
            if (vm.CancelPossible)
            {
                vm.CancelMedication();
                return false;
            }
            DisplayAlert("Änderung nicht gespeichert", "Sie müssen ihre Änderungen zuerst speichern.", "Okay");
            return true;
        }

        /// <summary>
        /// Methode, wenn der Abbrechen-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            vm.CancelMedication();
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Methode, wenn der Speichern-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        async void SaveMedication_Clicked(object sender, EventArgs e)
        {
            vm.SaveNewMedication();
            await Navigation.PopModalAsync();
        }

    }
}
