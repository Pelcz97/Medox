using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    [Preserve(AllMembers = true)]
    public partial class SendDataPage : CustomContentPage
    {
        public SendDataPage()
        {
            InitializeComponent();
        }

        void SendDataButton_Clicked(object sender, System.EventArgs e)
        {
            var page = new TransmittingDataPage();

            NavigationPage.SetBackButtonTitle(page, "Senden");
            Navigation.PushAsync(page);
        }
    }
}
