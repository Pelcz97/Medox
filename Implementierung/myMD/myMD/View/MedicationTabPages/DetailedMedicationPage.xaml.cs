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
    /// DetailedMedication Ansicht.
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
            this.BindingContext = vm;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.MedicationTabPages.DetailedMedicationPage"/> class.
        /// </summary>
        /// <param name="item">Item.</param>
        public DetailedMedicationPage(object item)
        {
            InitializeComponent();

            vm = new DetailedMedicineViewModel(item);
            this.BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn der Abbrechen-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void CancelButton_Clicked(object sender, System.EventArgs e)
        {
            vm.CancelMedication();
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Methode, wenn der Speichern-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void SaveMedication_Clicked(object sender, System.EventArgs e)
        {
            vm.SaveNewMedication();
            await Navigation.PopModalAsync();
        }

    }
}
