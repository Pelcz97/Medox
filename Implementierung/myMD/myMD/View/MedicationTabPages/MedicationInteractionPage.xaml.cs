using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using myMD.View.AbstractPages;
using myMD.ViewModel.MedicationTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.MedicationTabPages
{
    [Preserve(AllMembers = true)]
    public partial class MedicationInteractionPage : CustomContentPage
    {
        MedicationInteractionViewModel vm;
        public MedicationInteractionPage(object interactions)
        {
            InitializeComponent();
            vm = new MedicationInteractionViewModel(interactions);
            BindingContext = vm;
        }

        public MedicationInteractionPage()
        {
            InitializeComponent();
            vm = new MedicationInteractionViewModel(null);
            BindingContext = vm;
        }

        async void TranslateButton_Clicked(object sender, EventArgs e){
            try {
                vm.TranslateEntries();
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex);
                await DisplayAlert("Fehler", "Die Verbindung zum Server ist fehlgeschlagen. Prüfe deine Internetverbindung und probiere es erneut.", "Okay");
            }
        }
                                           
        async void DoneButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}