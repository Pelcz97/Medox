using System;
using System.Collections.Generic;
using System.ComponentModel;
using myMD.Model.DataModel;
using myMD.View.AbstractPages;
using myMD.ViewModel.MedicationTabViewModel;
using Xamarin.Forms;

namespace myMD.View.MedicationTabPages
{
    public partial class DetailedMedicationPage : CustomContentPage, INotifyPropertyChanged
    {
        DetailedMedicineViewModel vm;

        public DetailedMedicationPage()
        {
            InitializeComponent();

            vm = new DetailedMedicineViewModel(new Medication());
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
