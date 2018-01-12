using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using Xamarin.Forms;

namespace myMD.View.MedicationTabPages
{
    public partial class DetailedMedicationPage : CustomContentPage
    {

        public DetailedMedicationPage()
        {
            InitializeComponent();
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
