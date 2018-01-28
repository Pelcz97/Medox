using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    [Preserve(AllMembers = true)]
    public partial class SelectDoctorsLettersPage : CustomContentPage
    {
        SelectDoctorsLettersViewModel vm;
        public SelectDoctorsLettersPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }

        async void CancelSelectDoctorsLetters_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}