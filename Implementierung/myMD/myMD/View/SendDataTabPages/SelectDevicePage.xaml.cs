using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using Xamarin.Forms;
using myMD.ViewModel.SendDataTabViewModel;

namespace myMD.View.SendDataTabPages
{
    public partial class SelectDevicePage : CustomContentPage
    {
        public SelectDevicePage()
        {
            InitializeComponent();
            this.BindingContext = new SelectDeviceViewModel();
        }

        async void CancelSelectDevice_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
