using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using Xamarin.Forms;

namespace myMD.View.SendDataTabPages
{
    public partial class SelectDoctorsLettersPage : CustomContentPage
    {
        public SelectDoctorsLettersPage()
        {
            InitializeComponent();
        }

        async void CancelSelectDoctorsLetters_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}