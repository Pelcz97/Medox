using System;
using System.Collections.Generic;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using myMD.View.AbstractPages;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms;

namespace myMD.View.OverviewTabPages
{
    public partial class OverviewPage : CustomContentPage
    {
        public OverviewPage()
        {
            InitializeComponent();
            this.BindingContext = new OverviewViewModel(this.Navigation);
        }
        
        void DoctorsLetterSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            Navigation.PushModalAsync(new DetailedDoctorsLetterPage(DoctorsLettersList.SelectedItem));
        }

    }
}
