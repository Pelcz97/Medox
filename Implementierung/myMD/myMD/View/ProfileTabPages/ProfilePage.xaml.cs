using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.ProfileTabViewModel;
using Xamarin.Forms;

namespace myMD.View.ProfileTabPages
{
    public partial class ProfilePage : CustomContentPage { 

        ProfileViewModel vm;

        public ProfilePage()
        {
            InitializeComponent();
            ProfileViewModel vm = new ProfileViewModel();
            BindingContext = this.vm;
        }
    }
}
