using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using Xamarin.Forms;

namespace myMD.View.SendDataTabPages
{
    public partial class SelectDevicePage : CustomContentPage
    {
        public SelectDevicePage()
        {
            InitializeComponent();
        }

        async void CancelSelectDevice_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
