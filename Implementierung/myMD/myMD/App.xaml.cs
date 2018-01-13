using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using myMD.View.OverviewTabPages;
using myMD.View.MedicationTabPages;
using myMD.View.SendDataTabPages;
using myMD.View.ProfileTabPages;


namespace myMD
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var OverviewPage = new Xamarin.Forms.NavigationPage(new OverviewPage());
            var MedicationPage = new Xamarin.Forms.NavigationPage(new MedicationPage());
            var SendDataPage = new Xamarin.Forms.NavigationPage(new SendDataPage());
            var ProfilePage = new Xamarin.Forms.NavigationPage(new ProfilePage());

            OverviewPage.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);
            OverviewPage.SetValue(Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.StatusBarTextColorModeProperty, Color.White);
            OverviewPage.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            OverviewPage.BarTextColor = Color.FromHex("FFFFFF");

            MedicationPage.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);
            MedicationPage.SetValue(Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.StatusBarTextColorModeProperty, Color.White);
            MedicationPage.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            MedicationPage.BarTextColor = Color.FromHex("FFFFFF");

            SendDataPage.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);
            SendDataPage.SetValue(Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.StatusBarTextColorModeProperty, Color.White);
            SendDataPage.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            SendDataPage.BarTextColor = Color.FromHex("FFFFFF");

            ProfilePage.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);
            ProfilePage.SetValue(Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.StatusBarTextColorModeProperty, Color.White);
            ProfilePage.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            ProfilePage.BarTextColor = Color.FromHex("FFFFFF");

            var tabs = new TabbedPage();
            tabs.BarBackgroundColor = Color.FromRgb(25, 25, 40);

            tabs.Children.Add(OverviewPage);
            tabs.Children.Add(MedicationPage);
            tabs.Children.Add(SendDataPage);
            tabs.Children.Add(ProfilePage);

            tabs.Children[0].Title = "Übersicht";
            tabs.Children[1].Title = "Medikation";
            tabs.Children[2].Title = "Senden";
            tabs.Children[3].Title = "Profil";

            tabs.Children[0].Icon = "medicationTab2.png";
            tabs.Children[1].Icon = "medicationTab.png";
            tabs.Children[3].Icon = "medicationTab.png";
             
            MainPage = tabs;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
