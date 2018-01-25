using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using Xamarin.Forms;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    [Preserve(AllMembers = true)]
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
