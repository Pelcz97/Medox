using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;

using Xamarin.Forms;

namespace myMD.View.MedicationTabPages
{
    public partial class MedicationPage : CustomContentPage
    {
        public MedicationPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var view = new NavigationPage(new DetailedMedicationPage());
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;

            await Navigation.PushModalAsync(view);
        }

    }
}
