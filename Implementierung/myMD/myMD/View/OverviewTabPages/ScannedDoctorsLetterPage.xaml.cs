using System;
using System.Collections.Generic;
using System.Diagnostics;
using myMD.View.AbstractPages;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.OverviewTabPages
{
    [Preserve(AllMembers = true)]
    public partial class ScannedDoctorsLetterPage : CustomContentPage
    {
        ScannedDoctorsLetterViewModel vm;
        public ScannedDoctorsLetterPage(string diagnosis)
        {
            InitializeComponent();
            vm = new ScannedDoctorsLetterViewModel(diagnosis);
            BindingContext = vm;
        }

        async void SaveLetter_Clicked(object sender, System.EventArgs e)
        {
            vm.SaveNewDoctorsLetter();
            await Navigation.PopModalAsync();
        }
    }
}
