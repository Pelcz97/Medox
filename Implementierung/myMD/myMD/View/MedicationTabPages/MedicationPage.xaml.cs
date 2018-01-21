using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.MedicationTabViewModel;
using Xamarin.Forms;

namespace myMD.View.MedicationTabPages
{
    public partial class MedicationPage : CustomContentPage
    {
        MedicationViewModel vm;

        public MedicationPage()
        {
            InitializeComponent();
            vm = new MedicationViewModel();
            this.BindingContext = vm;
        }

        public async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var view = new NavigationPage(new DetailedMedicationPage());
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;

            await Navigation.PushModalAsync(view);
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //MessagingCenter.Send<MedicationPage, object>(this, "SelectedMedication", e.SelectedItem);
            //MessagingCenter.Unsubscribe<MedicationPage, object>(this, "SelectedMedication");
        }
    }
}
