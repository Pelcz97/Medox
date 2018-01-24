using System;
using System.Collections.Generic;
using System.Diagnostics;
using myMD.View.AbstractPages;
using myMD.ViewModel.MedicationTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.MedicationTabPages
{
    [Preserve(AllMembers = true)]
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
            var view = new NavigationPage(new DetailedMedicationPage(e.SelectedItem));
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;
            Navigation.PushModalAsync(view);
        }
    }
}
