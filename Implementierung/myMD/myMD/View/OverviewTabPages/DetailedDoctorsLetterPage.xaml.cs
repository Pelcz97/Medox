using System;
using System.Collections.Generic;
using ModelInterface.DataModelInterface;
using myMD.View.AbstractPages;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms;

namespace myMD.View.OverviewTabPages
{
    public partial class DetailedDoctorsLetterPage : CustomContentPage
    {
        public DetailedDoctorsLetterPage(object e)
        {
            InitializeComponent();
            this.BindingContext = new DetailedDoctorsLetterViewModel(e);
        }

        async void CancelDetailedPage_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
