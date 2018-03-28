using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace myMD.ViewModel.OverviewTabViewModel
{
    public class KameraPageViewModel : ContentPage
    {

        public Image Image { get; set; }

        public MediaFile File { get; set; }

        public KameraPageViewModel()
        {
            
        }
    }
}