using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.ProfileTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.ProfileTabPages
{
    [Preserve(AllMembers = true)]
    public partial class ProfileEditPage : CustomContentPage
    {
        ProfileEditViewModel vm;
        public ProfileEditPage()
        {
            InitializeComponent();
            vm = new ProfileEditViewModel();
            this.BindingContext = vm;
        }

        async void CancelButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveChangedProfileButton_Clicked(object sender, System.EventArgs e)
        {
            vm.SaveChangedProfile();
            await Navigation.PopModalAsync();
        }
    }
}
