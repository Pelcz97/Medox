﻿using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.ProfileTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.ProfileTabPages
{
    [Preserve(AllMembers = true)]
    public partial class DictionaryPage : CustomContentPage
    {
        DictionaryViewModel vm;
        public DictionaryPage()
        {
            InitializeComponent();
            vm = new DictionaryViewModel();
            BindingContext = vm;
        }

        async void DoneButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {
            var input = SearchBar.Text;

            if(input.Length != 0){
                vm.GetDefinition(input);
            }
        }
    }
}
