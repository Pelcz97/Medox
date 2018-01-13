using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using Xamarin.Forms;

namespace myMD.View.SendDataTabPages
{
    public partial class SendDataPage : CustomContentPage
    {
        public SendDataPage()
        {
            InitializeComponent();
        }

        void SendDataButton_Clicked(object sender, System.EventArgs e)
        {
            var page = new SelectDoctorsLettersPage();

            NavigationPage.SetBackButtonTitle(page, "Hmm");
            Navigation.PushAsync(page);
        }
    }
}
