using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms;

namespace myMD.View.OverviewTabPages
{
    public partial class ScannedDoctorsLetterPage : CustomContentPage
    {
        ScannedDoctorsLetterViewModel vm;
        public ScannedDoctorsLetterPage()
        {
            InitializeComponent();
            vm = new ScannedDoctorsLetterViewModel();
            BindingContext = vm;
        }


    }
}
