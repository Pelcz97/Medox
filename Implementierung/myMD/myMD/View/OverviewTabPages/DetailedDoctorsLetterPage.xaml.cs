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
        public DetailedDoctorsLetterPage(object item)
        {
            InitializeComponent();
            this.BindingContext = new DetailedDoctorsLetterViewModel(item);
            var Image = ImageSource.FromFile("cancel.png");
            QuitButton.Image = (FileImageSource)Image;
        }

        async void CancelDetailedPage_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
