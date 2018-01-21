using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using myMD.View.OverviewTabPages;
using myMD.View.MedicationTabPages;
using myMD.View.SendDataTabPages;
using myMD.View.ProfileTabPages;
using Autofac;
using Autofac.Core;
using myMD.ViewModel.OverallViewModel;
using myMD.Model.ModelFacade;
using myMD.ModelInterface.ModelFacadeInterface;
using myMD.Model.TransmissionModel;
using myMD.Model.DatabaseModel;
using myMD.Model.ParserModel;
using myMD.Model.EntityFactory;

namespace myMD
{
    public partial class App : Application
    {
        /// <summary>
        /// IoC Container für Dependency Injection im Model
        /// </summary>
        private static IContainer container;

        public App()
        {
            PrepareContainer();
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

        /// <summary>
        /// Registriere Abhängigkeiten des Models und baue den Container
        /// </summary>
        private static void PrepareContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ModelFacade>().As<IModelFacade>().SingleInstance();
            containerBuilder.RegisterType<Bluetooth>().As<IBluetooth>().SingleInstance();
            containerBuilder.RegisterType<EntityDatabase>().As<IEntityDatabase>().SingleInstance();
            containerBuilder.RegisterType<EntityFactory>().As<IEntityFactory>().SingleInstance();
            containerBuilder.RegisterType<ParserFacade>().As<IParserFacade>().SingleInstance();
            container = containerBuilder.Build();
        }

        /// <summary>
        /// Löse Abhängigkeiten im Model auf
        /// </summary>
        /// <returns>Instanz des Models</returns>
        public static IModelFacade GetModel()
        {
            return container.Resolve<IModelFacade>();
        }

    }
}
