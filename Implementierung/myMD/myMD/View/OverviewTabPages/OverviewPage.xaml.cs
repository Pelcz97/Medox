using System;
using System.Collections.Generic;
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
            this.BindingContext = new OverviewViewModel();
        }


    }
}
