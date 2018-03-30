using myMD.View.AbstractPages;
using myMD.ViewModel.OverviewTabViewModel;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace myMD.View.OverviewTabPages
{
    [Preserve(AllMembers = true)]
    public partial class KameraPage : CustomContentPage
    {
        KameraViewModel vm;
        public KameraPage()
        {
            InitializeComponent();
            vm = new KameraViewModel();
            BindingContext = vm;
            LoadingIndicator.IsVisible = false;
        }


        async void CancelButton_Clicked(object sender, EventArgs e){
            await Navigation.PopModalAsync();
        }

        async void SelectImage_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayActionSheet(null, "Abbrechen", null, "Foto aufnehmen", "Fotomediathek");
            switch (answer)
            {
                case "Fotomediathek":
                    PickPhoto();
                    break;
                case "Foto aufnehmen":
                    TakePhoto();
                    break;
                default:
                    break;
            }
        }

        async void TakePhoto()
        {

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            ImageSource source = await vm.TakePhoto();
            if (source != null){
                image.Source = source;
            }
        }

        async void PickPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");
                return;
            }

            ImageSource source = await vm.PickPhoto();
            if (source != null)
            {
                image.Source = source;
            }
        }

        async void ScanButton_Clicked(object sender, System.EventArgs e)
        {
            SelectImageButton.IsVisible = false;
            LoadingIndicator.IsVisible = true;
            //string diagnosis = await vm.ScanImage();
            string diagnosis = "Platzhalter";

            var page = new ScannedDoctorsLetterPage(diagnosis);
            page.On<iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(page, "Bild wählen");
            await Navigation.PushAsync(page);

            LoadingIndicator.IsVisible = false;
            SelectImageButton.IsVisible = true;

        }
    }
}