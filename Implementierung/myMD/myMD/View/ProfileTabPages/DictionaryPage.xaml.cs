using System;
using System.Collections.Generic;
using System.Net.Http;
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

        async void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {
            var input = SearchBar.Text;

            if (input.Length != 0)
            {

                var result = await vm.GetDefinition(input);
                if (result == 0)
                    await DisplayAlert("Fehler", "Etwas scheint nicht in Ordnung zu sein. Prüfe deine Internetverbindung oder versuche es später nocheinmal.", "Okay");

            }
        }
    }
}
