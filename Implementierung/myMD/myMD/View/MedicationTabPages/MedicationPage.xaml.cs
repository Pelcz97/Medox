using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using ViewModel.MedicationTabViewModel;
using Xamarin.Forms;

namespace myMD.View.MedicationTabPages
{
    public partial class MedicationPage : CustomContentPage
    {
        public MedicationPage()
        {
            InitializeComponent();
            BindingContext = new MedicationViewModel(Navigation);
        }


    }
}
