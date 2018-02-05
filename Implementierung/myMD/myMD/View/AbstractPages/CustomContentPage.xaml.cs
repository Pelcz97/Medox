using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace myMD.View.AbstractPages
{
    /// <summary>
    /// Code-Behind Klasse zur CostumContentPage
    /// </summary>
    public partial class CustomContentPage : ContentPage
    {
        /// <summary>
        /// Konstruktor für eine CostumContentPage
        /// </summary>
        public CustomContentPage()
        {
            InitializeComponent();
            this.BackgroundColor = Color.FromRgb(25, 25, 40);
            this.SetValue(Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.StatusBarTextColorModeProperty, Color.White);
        }
    }
}
