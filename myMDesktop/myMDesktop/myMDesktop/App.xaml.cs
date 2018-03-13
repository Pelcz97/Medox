using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;


using Xamarin.Forms;

namespace myMDesktop
{
	public partial class App : Application
	{
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new myMDesktop.View.SendDataTabPages.SendDataPage())
            {
                BarBackgroundColor = Color.FromHex("#191928")
            };
        
		}

		protected override void OnStart ()
		{
            AppCenter.Start("uwp=ae142cce-a75a-4de6-bbff-30ba30e78c28;",
                  typeof(Analytics));
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
