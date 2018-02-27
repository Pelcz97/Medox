
using myMD.Model.DatabaseModel;
using myMD.Model.DependencyService;
using myMD.Model.EntityFactory;
using myMD.Model.ModelFacade;
using myMD.Model.ParserModel;
using myMD.Model.TransmissionModel;
using myMD.ModelInterface.ModelFacadeInterface;
using myMD.View.MedicationTabPages;
using myMD.View.OverviewTabPages;
using myMD.View.ProfileTabPages;
using myMD.View.SendDataTabPages;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

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

            var tabs = new TabbedPage
            {
                BarBackgroundColor = Color.FromRgb(25, 25, 40)
            };

            tabs.Children.Add(OverviewPage);
            tabs.Children.Add(MedicationPage);
            tabs.Children.Add(SendDataPage);
            tabs.Children.Add(ProfilePage);

            tabs.Children[0].Title = "Übersicht";
            tabs.Children[1].Title = "Medikation";
            tabs.Children[2].Title = "Senden";
            tabs.Children[3].Title = "Profil";

            MainPage = tabs;
        }

        /// <summary>
        /// Model Einzelstück.
        /// </summary>
        public static IModelFacade Model { get; } = CreateModel();

        protected override void OnResume()
        {
            
        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=9689ec9a-a2df-413d-848c-9a6885c7bd58;" + 
                            "android=8b0b56f0-78d0-4071-927b-e2023ff52fae;",
                            typeof(Analytics), typeof(Crashes));
        }

        /// <summary>
        /// Erstellt neues Model. Wird einmalig zum Initialisieren der Model Property aufgerufen.
        /// </summary>
        /// <returns>Neue Instanz des Models</returns>
        private static IModelFacade CreateModel()
        {
            DependencyServiceWrapper.Service = new XamarinDependencyService();
            return new ModelFacade(new EntityDatabase(), new EntityFactory(), new ParserFacade(), new Bluetooth());
        }
    }
}