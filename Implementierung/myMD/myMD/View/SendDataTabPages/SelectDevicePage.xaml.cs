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
        SelectDeviceViewModel vm;
        public SelectDevicePage()
        {
            InitializeComponent();
            vm = new SelectDeviceViewModel();
            this.BindingContext = vm;
        }

        async void CancelSelectDevice_Clicked(object sender, System.EventArgs e)
        {
            vm.StopScan();
            await Navigation.PopModalAsync();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            vm.ConnectToDevice(e.SelectedItem);
        }
    }
}
