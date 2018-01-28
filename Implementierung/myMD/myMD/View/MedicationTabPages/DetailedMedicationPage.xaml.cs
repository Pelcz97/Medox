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
    [Preserve(AllMembers = true)]
    public partial class DetailedMedicationPage : CustomContentPage
    {
        DetailedMedicineViewModel vm;

        public DetailedMedicationPage()
        {
            InitializeComponent();

            vm = new DetailedMedicineViewModel();
            this.BindingContext = vm;
        }

        public DetailedMedicationPage(object item)
        {
            InitializeComponent();

            vm = new DetailedMedicineViewModel(item);
            this.BindingContext = vm;
        }

        async void CancelButton_Clicked(object sender, System.EventArgs e)
        {
            vm.cancelMedication();
            await Navigation.PopModalAsync();
        }

        async void SaveMedication_Clicked(object sender, System.EventArgs e)
        {
            vm.SaveNewMedication();
            await Navigation.PopModalAsync();
        }

    }
}
