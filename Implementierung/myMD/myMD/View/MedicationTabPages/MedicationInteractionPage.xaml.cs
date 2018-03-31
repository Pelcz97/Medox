using System;
using System.Collections.Generic;
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

        async void DoneButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}