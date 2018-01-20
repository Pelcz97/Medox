using System;
using System.Collections.Generic;
using System.ComponentModel;
using myMD.Model.DataModel;
using myMD.View.AbstractPages;
using myMD.ViewModel.MedicationTabViewModel;
using Xamarin.Forms;
using myMD.ModelInterface.DataModelInterface;

namespace myMD.View.MedicationTabPages
{
    public partial class DetailedMedicationPage : CustomContentPage
    {
        DetailedMedicineViewModel vm;

        public DetailedMedicationPage()
        {
            InitializeComponent();

            vm = new DetailedMedicineViewModel();
            this.BindingContext = vm;
        }

        async void CancelButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveMedication_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}
