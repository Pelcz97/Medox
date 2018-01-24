using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    [Preserve(AllMembers = true)]
    public partial class TransmittingDataPage : CustomContentPage
    {
        public TransmittingDataPage()
        {
            InitializeComponent();
        }

        public async void SelectLettersButton_Clicked(object sender, System.EventArgs e)
        {
            var view = new NavigationPage(new SelectDoctorsLettersPage());
            view.BarTextColor = Color.White;
            view.BarBackgroundColor = Color.FromRgb(15, 15, 40);
            await Navigation.PushModalAsync(view);
        }

        public async void SelectDeviceButton_Clicked(object sender, System.EventArgs e)
        {
            var view = new NavigationPage(new SelectDevicePage()); 
            view.BarTextColor = Color.White;
            view.BarBackgroundColor = Color.FromRgb(15, 15, 40);
            await Navigation.PushModalAsync(view);
        }
    }
}
