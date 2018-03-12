using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace myMD.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);
            UITabBar.Appearance.BackgroundColor = UIColor.FromRGB(25, 25, 40);
            UITabBar.Appearance.TintColor = UIColor.FromRGB(0, 185, 180);
            global::Xamarin.Forms.Forms.Init();

            // Color of the unselected tab icon & text:
            /*UITabBarItem.Appearance.SetTitleTextAttributes(
                new UITextAttributes()
                {
                    TextColor = UIColor.FromRGB(160, 170, 180)
                },
                UIControlState.Normal);*/

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
			Xamarin.Calabash.Start();
#endif
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);


        }
    }
}
