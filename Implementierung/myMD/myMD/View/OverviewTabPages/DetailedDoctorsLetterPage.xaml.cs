using System;
using System.Collections.Generic;
using myMD.ModelInterface.DataModelInterface;
using myMD.View.AbstractPages;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.OverviewTabPages
{
    [Preserve(AllMembers = true)]
    public partial class DetailedDoctorsLetterPage : CustomContentPage
    {
        public DetailedDoctorsLetterPage(object obj)
        {
            InitializeComponent();
            this.BindingContext = new DetailedDoctorsLetterViewModel(obj);
        }

        async void CancelDetailedPage_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
