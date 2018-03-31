﻿using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.MedicationTabViewModel;
using Xamarin.Forms;

namespace myMD.View.MedicationTabPages
{
    public partial class MedicationInteractionPage : CustomContentPage
    {
        MedicationInteractionViewModel vm;
        public MedicationInteractionPage(object interactions)
        {
            InitializeComponent();
            vm = new MedicationInteractionViewModel(interactions);
            BindingContext = vm;
        }

        async void DoneButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
