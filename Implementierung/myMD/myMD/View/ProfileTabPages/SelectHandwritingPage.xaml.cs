﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using myMD.View.AbstractPages;
using myMD.ViewModel.ProfileTabViewModel;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.ProfileTabPages
{
    [Preserve(AllMembers = true)]
    public partial class SelectHandwritingPage : CustomContentPage
    {
        SelectHandwritingViewModel vm;
        public SelectHandwritingPage()
        {
            InitializeComponent();
            vm = new SelectHandwritingViewModel();
            BindingContext = vm;
            LoadingIndicator.IsVisible = false;
        }

        async void CancelButton_Clicked(object sender, EventArgs e)
        {
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
            if (source != null)
            {
                image.Source = source;
            }
        }

        async void PickPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "Okay");
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

            try
            {
                string diagnosis = await vm.ScanImage();
                //string diagnosis = "Platzhalter";

                var page = new OverviewTabPages.ScannedDoctorsLetterPage(diagnosis);
                NavigationPage.SetBackButtonTitle(page, "Bild wählen");
                await Navigation.PushAsync(page);

                LoadingIndicator.IsVisible = false;
                SelectImageButton.IsVisible = true;
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex);
                await DisplayAlert("Fehler", "Die Verbindung zum Server ist fehlgeschlagen. Prüfe deine Internetverbindung und probiere es erneut.", "Okay");
            }

        }
    }
}
