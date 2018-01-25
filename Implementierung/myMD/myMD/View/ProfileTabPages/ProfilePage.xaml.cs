using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.ProfileTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.ProfileTabPages
{
    [Preserve(AllMembers = true)]
    public partial class ProfilePage : CustomContentPage
    {
        ProfileViewModel vm;
        public ProfilePage()
        {
            InitializeComponent();

            vm = new ProfileViewModel();
            BindingContext = vm;
        }
    }
}
